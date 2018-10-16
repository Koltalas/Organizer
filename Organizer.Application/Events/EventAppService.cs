using Abp.Domain.Repositories;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organizer.Events.Dto;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using Abp.Authorization;
using static Organizer.OrganizerConsts;
using Organizer.Users;

namespace Organizer.Events
{
    [AbpAuthorize]
    public class EventAppService : OrganizerAppServiceBase, IEventAppService
    {
        private readonly IEventManager _eventManager;
        private readonly IRepository<Event, Guid> _eventRepository;
        private readonly IRepository<EventList, Guid> _eventListRepository;


        public EventAppService(
            IEventManager eventManager,
            IRepository<Event, Guid> eventRepository,
            IRepository<EventList, Guid> eventListRepository)
        {
            _eventManager = eventManager;
            _eventRepository = eventRepository;
            _eventListRepository = eventListRepository;
        }

        public async Task<ListResultDto<EventListDetailOutput>> GetAllList()
        {
            var eventLists = await _eventManager.GetUserListsAsync(await GetCurrentUserAsync());

            return new ListResultDto<EventListDetailOutput>(eventLists.Select(e => e.IsOwner(AbpSession.UserId)).MapTo<List<EventListDetailOutput>>());
        }

        public async Task<ListResultDto<EventDetailOutput>> GetAllEvents()
        {
            var events = (await _eventManager.GetUserListsAsync(await GetCurrentUserAsync())).SelectMany(e => e.Events);

            return new ListResultDto<EventDetailOutput>(events.MapTo<List<EventDetailOutput>>());
        }

        public async Task<EventListDetailOutput> GetListDetail(EntityDto<Guid> input)
        {
            var list = (await _eventManager.GetUserListsAsync(await GetCurrentUserAsync()))
                .Where(e => e.Id == input.Id);

            if (list == null)
                throw new UserFriendlyException("Could not found the event list, maybe it's deleted.");

            return list.MapTo<EventListDetailOutput>();
        }


        public async Task<EventDetailOutput> GetEventDetail(EntityDto<Guid> input)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(input.Id);

            if (@event == null)
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");

            return @event.MapTo<EventDetailOutput>();
        }


        public async Task<EventListDetailOutput> CreateList(CreateListInput input)
        {
            var eventList = EventList.Create(input.Title, input.Description, input.Color);

            if (input.Events != null)
            {
                foreach (var elem in input.Events)
                {
                    var @event = Event.Create(elem.Title, elem.Start, elem.AllDay, eventList.Id, elem.Description,
                        elem.End);
                    eventList.Add(@event);
                }
            }

            var newList = await _eventManager.CreateListAsync(eventList);
            await _eventManager.AddUserAsync(eventList, await GetCurrentUserAsync(), SharingType.Owner);
            return newList.MapTo<EventListDetailOutput>();
        }

        public async Task<EventDetailOutput> CreateEvent(CreateEventInput input)
        {
            var list = await _eventListRepository.FirstOrDefaultAsync(input.EventListId);

            if (list == null)
                throw new UserFriendlyException("Could not to find the event list to attach");
            
            var @event = Event.Create(input.Title, input.Start, input.AllDay, input.EventListId, input.Description,
                input.End);


            var newEvent = await _eventManager.CreateEventAsync(@event);

            return newEvent.MapTo<EventDetailOutput>();
        }

        public async Task DeleteList(EntityDto<Guid> input)
        {
            var eventList = await _eventListRepository.FirstOrDefaultAsync(input.Id);

            if (eventList == null)
                throw new UserFriendlyException("Could not find the EventList, maybe it's already deleted.");

            var listUsers = await _eventManager.GetListUsersAsync(eventList);

            if (!listUsers.Contains(await GetCurrentUserAsync()))
                throw new UserFriendlyException("You don`t have permission to this list.");

            await _eventManager.DeleteListAsync(eventList);
        }

        public async Task DeleteEvent(EntityDto<Guid> input)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(input.Id);

            if (@event == null)
                throw new UserFriendlyException("Could not found the event, maybe it's already deleted.");

            await _eventManager.DeleteEventAsync(@event);
        }

        public async Task UpdateList(UpdateEventListInput input)
        {
            var list = await _eventListRepository.FirstOrDefaultAsync(input.Id);

            if (list == null)
                throw new UserFriendlyException("Could not found the EventList, maybe it's deleted.");

            var listUsers = await _eventManager.GetListUsersAsync(list);

            if (!listUsers.Contains(await GetCurrentUserAsync()))
                throw new UserFriendlyException("You don`t have permission to this list.");

            list.Update(input.Title, input.Description, input.Color);
            await _eventManager.UpdateListAsync(list);
        }

        public async Task UpdateEvent(UpdateEventInput input)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(e => e.Id == input.Id);

            if (@event == null)
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");

            @event.Update(input.Title, input.Description, input.Start, input.End, input.AllDay, input.ListId);
            await _eventManager.UpdateEventAsync(@event);
        }

        public async Task<AddUserToListOutput> AddUserToList(AddUserToListInput input)
        {
            var eventListUser = await AddUserAndSaveAsync(
                await _eventManager.GetListAsync(input.EventListId),
                await UserManager.GetUserByIdAsync(input.UserId),
                input.SharingType);

            return new AddUserToListOutput
            {
                Id = eventListUser.Id
            };
        }

        public async Task RemoveUserFromList(RemoveUserFromListInput input)
        {
            await _eventManager.RemoveUserAsync(
                await _eventManager.GetListAsync(input.EventListId),
                await UserManager.GetUserByIdAsync(input.UserId));
        }

        private async Task<EventListUser> AddUserAndSaveAsync(EventList list, User user, SharingType sharingType)
        {
            var eventListUser = await _eventManager.AddUserAsync(list, user, sharingType);
            await CurrentUnitOfWork.SaveChangesAsync();
            return eventListUser;
        }


        public async Task<GenereateSharingOutput> GenerateSharing(GenerateSharingInput input)
        {
            var eventList = await _eventListRepository.FirstOrDefaultAsync(input.EventListId);
            var key = eventList.GenerateSharing(input.SharingPassword);
            return new GenereateSharingOutput
            {
                SharingKey = key
            };
        }

        public async Task<EventListDetailOutput> GetAccessToList(GetAccessToListInput input)
        {
            var eventList = await _eventListRepository.FirstOrDefaultAsync(e => e.SharingKey == input.SharingKey);

            if (eventList == null)
                throw new UserFriendlyException("Could not found the eventlist b tour key.");

            if (eventList.ListUsers.Select(e => e.User).Contains(await GetCurrentUserAsync()))
                throw new UserFriendlyException("You already have access to this list!");

            if (eventList.CheckPassword(input.SharingPassword))
                throw new UserFriendlyException("Wrong password!");

            await AddUserAndSaveAsync(
               eventList,
               await GetCurrentUserAsync(),
               SharingType.Client);

            return eventList.MapTo<EventListDetailOutput>();

        }
    }
}
