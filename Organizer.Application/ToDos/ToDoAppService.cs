using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;
using Organizer.ToDos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organizer.Users;
using static Organizer.OrganizerConsts;

namespace Organizer.ToDos
{

    [AbpAuthorize]
    public class ToDoAppService : OrganizerAppServiceBase, IToDoAppService
    {
        private readonly IToDoManager _toDoManager;
        private readonly IRepository<ToDoElem, Guid> _toDoElemRepository;
        private readonly IRepository<ToDoList, Guid> _toDoListRepository;


        public ToDoAppService(
            IToDoManager toDoManager,
            IRepository<ToDoElem, Guid> toDoElemRepository,
            IRepository<ToDoList, Guid> toDoListRepository)
        {
            _toDoManager = toDoManager;
            _toDoElemRepository = toDoElemRepository;
            _toDoListRepository = toDoListRepository;
        }

        public async Task<ToDoListDetailOutput> CreateList(CreateListInput input)
        {
            var toDoList = ToDoList.Create(input.Title, input.Description);

            if (input.Elems != null)
            {
                foreach (var elem in input.Elems)
                {
                    var toDoElem = ToDoElem.Create(elem.Title, toDoList.Id, elem.IsCompleted);
                    toDoList.Add(toDoElem);
                }
            }

            var newList = await _toDoManager.CreateListAsync(toDoList);
            await _toDoManager.AddUserAsync(toDoList, await GetCurrentUserAsync(), SharingType.Owner);
            return newList.MapTo<ToDoListDetailOutput>();
        }


        public async Task<ToDoElemDetailOutput> CreateElem(CreateElemInput input)
        {
            var list = await _toDoListRepository.FirstOrDefaultAsync(input.ToDoListId);

            if (list == null)
                throw new UserFriendlyException("The list is a lie");


            var toDoElem = ToDoElem.Create(input.Title, input.ToDoListId, input.IsCompleted);

            var newElem = await _toDoManager.CreateElemAsync(toDoElem);
            return newElem.MapTo<ToDoElemDetailOutput>();
        }

        public async Task DeleteList(EntityDto<Guid> input)
        {
            var toDoList = await _toDoListRepository.FirstOrDefaultAsync(input.Id);

            if (toDoList == null)
                throw new UserFriendlyException("Could not find the ToDoList, maybe it's already deleted.");

            var listUsers = await _toDoManager.GetListUsersAsync(toDoList);

            if (!listUsers.Contains(await GetCurrentUserAsync()))
                throw new UserFriendlyException("You don`t have permission to this list.");

            await _toDoManager.DeleteListAsync(toDoList);
        }

        public async Task DeleteElem(EntityDto<Guid> input)
        {
            var toDoElem = await _toDoElemRepository.FirstOrDefaultAsync(input.Id);

            if (toDoElem == null)
                throw new UserFriendlyException("Could not find the ToDoElem, maybe it's already deleted.");

            await _toDoManager.DeleteElemAsync(toDoElem);
        }

        public async Task<ListResultDto<ToDoListDetailOutput>> GetAllLists()
        {
            var toDoLists = await _toDoManager.GetUserListsAsync(await GetCurrentUserAsync());

            return new ListResultDto<ToDoListDetailOutput>(toDoLists.MapTo<List<ToDoListDetailOutput>>());


        }


        public async Task<ListResultDto<ToDoElemDetailOutput>> GetAllElems()
        {
            var toDoElems =
                (await _toDoManager.GetUserListsAsync(await GetCurrentUserAsync())).SelectMany(e => e.Elems);

            return new ListResultDto<ToDoElemDetailOutput>(toDoElems.MapTo<List<ToDoElemDetailOutput>>());
        }

        public async Task<ToDoListDetailOutput> GetListDetail(EntityDto<Guid> input)
        {
            var list = (await _toDoManager.GetUserListsAsync(await GetCurrentUserAsync())).Where(e => e.Id == input.Id);

            if (list == null)
                throw new UserFriendlyException("Could not found the ToDoList, maybe it's deleted.");

            return list.MapTo<ToDoListDetailOutput>();
        }

        public async Task<ToDoElemDetailOutput> GetElemDetail(EntityDto<Guid> input)
        {
            var elem = await _toDoElemRepository.FirstOrDefaultAsync(input.Id);

            if (elem == null)
                throw new UserFriendlyException("Could not found the ToDoElem, maybe it's deleted.");

            return elem.MapTo<ToDoElemDetailOutput>();
        }

        public async Task UpdateList(UpdateToDoListInput input)
        {
            var list = await _toDoListRepository.FirstOrDefaultAsync(input.Id);

            if (list == null)
                throw new UserFriendlyException("Could not found the ToDoList, maybe it's deleted.");

            var listUsers = await _toDoManager.GetListUsersAsync(list);

            if (!listUsers.Contains(await GetCurrentUserAsync()))
                throw new UserFriendlyException("You don`t have permission to this list.");

            list.Update(input.Title, input.Description);
            await _toDoManager.UpdateListAsync(list);
        }

        public async Task UpdateElem(UpdateToDoElemInput input)
        {
            var elem = await _toDoElemRepository.FirstOrDefaultAsync(input.Id);

            if (elem == null)
                throw new UserFriendlyException("Could not found the ToDoElem, maybe it's deleted.");

            elem.Update(input.Title, input.IsCompleted, input.ListId);
            await _toDoManager.UpdateElemAsync(elem);
        }

        public async Task<AddUserToListOutput> AddUserToList(AddUserToListInput input)
        {
            var toDoListUser = await AddUserAndSaveAsync(
                await _toDoManager.GetListAsync(input.ToDoListId),
                await UserManager.GetUserByIdAsync(input.UserId),
                input.SharingType);

            return new AddUserToListOutput
            {
                Id = toDoListUser.Id
            };
        }

        public async Task RemoveUserFromList(RemoveUserFromListInput input)
        {
            await _toDoManager.RemoveUserAsync(
                await _toDoManager.GetListAsync(input.ToDoListId),
                await UserManager.GetUserByIdAsync(input.UserId));
        }

        private async Task<ToDoListUser> AddUserAndSaveAsync(ToDoList list, User user, SharingType sharingType)
        {
            var toDoListUser = await _toDoManager.AddUserAsync(list, user, sharingType);
            await CurrentUnitOfWork.SaveChangesAsync();
            return toDoListUser;
        }


        public async Task<GenereateSharingOutput> GenerateSharing(GenerateSharingInput input)
        {
            var toDoList = await _toDoListRepository.FirstOrDefaultAsync(input.ToDoListId);
            var key = toDoList.GenerateSharing(input.SharingPassword);
            return new GenereateSharingOutput
            {
                SharingKey = key
            };
        }

        public async Task<ToDoListDetailOutput> GetAccessToList(GetAccessToListInput input)
        {
            var toDoList = await _toDoListRepository.FirstOrDefaultAsync(e => e.SharingKey == input.SharingKey);

            if (toDoList == null)
                throw new UserFriendlyException("Could not found the eventlist with this key.");

            if (toDoList.ListUsers.Select(e => e.User).Contains(await GetCurrentUserAsync()))
                throw new UserFriendlyException("You already have access to this list!");


            if (toDoList.CheckPassword(input.SharingPassword))
                    throw new UserFriendlyException("Wrong password!");

            await AddUserAndSaveAsync(
                toDoList,
                await GetCurrentUserAsync(),
                SharingType.Client);

            return toDoList.MapTo<ToDoListDetailOutput>();

        }

        public async Task AddReferenceToEvent(AddReferenceToEventInput input)
        {
            var toDoList = await _toDoListRepository.FirstOrDefaultAsync(input.ToDoListId);
            if (toDoList == null)
                throw new UserFriendlyException("Could not found the eventlist.");
            
            toDoList.AddReferenceToEvent(input.EventId);
            
        }
    }
}
