using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Organizer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organizer.Events;
using static Organizer.OrganizerConsts;

namespace Organizer.ToDos
{
    public class ToDoManager : IToDoManager
    {
        public IEventBus EventBus { get; set; }

        private readonly IRepository<ToDoElem, Guid> _toDoElemRepository;
        private readonly IRepository<ToDoList, Guid> _toDoListRepository;
        private readonly IRepository<ToDoListUser> _toDoListUserRepository;

        public ToDoManager(
            IRepository<ToDoElem, Guid> toDoElemRepository,
            IRepository<ToDoList, Guid> toDoListRepository,
            IRepository<ToDoListUser> toDoListUserRepository)
        {
            _toDoElemRepository = toDoElemRepository;
            _toDoListRepository = toDoListRepository;
            _toDoListUserRepository = toDoListUserRepository;

            EventBus = NullEventBus.Instance;
        }

        public async Task<ToDoElem> GetElemAsync(Guid id)
        {
            var elem = await _toDoElemRepository.FirstOrDefaultAsync(id);

            if (elem == null)
                throw new UserFriendlyException("Could not found the ToDoElem, maybe it's deleted!");

            return elem;
        }

        public async Task<ToDoList> GetListAsync(Guid id)
        {
            var list = await _toDoListRepository.FirstOrDefaultAsync(id);

            if (list == null)
                throw new UserFriendlyException("Could not found the ToDoList, maybe it's deleted!");

            return list;
        }

        public async Task<ToDoElem> CreateElemAsync(ToDoElem elem)
        {
            var newElem= await _toDoElemRepository.InsertAsync(elem);
            return newElem;
        }

        public async Task<ToDoList> CreateListAsync(ToDoList list)
        {
            var newList = await _toDoListRepository.InsertAsync(list);
            return newList;
        }

        public async Task DeleteElemAsync(ToDoElem elem)
        {
            await _toDoElemRepository.DeleteAsync(elem);
        }

        public async Task DeleteListAsync(ToDoList list)
        {
            await _toDoListRepository.DeleteAsync(list);
        }

        public async Task UpdateElemAsync(ToDoElem elem)
        {
            await _toDoElemRepository.UpdateAsync(elem);
        }

        public async Task UpdateListAsync(ToDoList list)
        {
            await _toDoListRepository.UpdateAsync(list);
        }

        public async Task<ToDoListUser> AddUserAsync(ToDoList toDoList, User user, SharingType sharingType = SharingType.Client)
        {
            return await _toDoListUserRepository.InsertAsync(
                ToDoListUser.Create(toDoList, user, sharingType));
        }

        public async Task RemoveUserAsync(ToDoList toDoList, User user)
        {
            var toDoListUser = await _toDoListUserRepository.FirstOrDefaultAsync(
                e => e.ToDoListId == toDoList.Id && e.UserId == user.Id);

            if (toDoListUser == null)
                return;

            await toDoListUser.RemoveAsync(_toDoListUserRepository);
        }

        public async Task<IReadOnlyList<User>> GetListUsersAsync(ToDoList toDoList)
        {
            return (await _toDoListUserRepository.GetAllListAsync(e => e.ToDoListId == toDoList.Id)).Select(r => r.User).ToList();
        }

        public async Task<IReadOnlyList<ToDoList>> GetUserListsAsync(User user)
        {
            return (await _toDoListUserRepository.GetAllListAsync(e => e.UserId == user.Id)).Select(r => r.ToDoList).ToList();
        }
        

    }
}
