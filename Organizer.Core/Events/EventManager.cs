using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Organizer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Organizer.OrganizerConsts;

namespace Organizer.Events
{
    public class EventManager : IEventManager
    {
        public IEventBus EventBus { get; set; }

        private readonly IRepository<Event, Guid> _eventRepository;
        private readonly IRepository<EventList, Guid> _eventListRepository;
        private readonly IRepository<EventListUser> _eventListUserRepository;


        public EventManager(
            IRepository<Event, Guid> eventRepository,
            IRepository<EventList, Guid> eventListRepository,
            IRepository<EventListUser> eventListUserRepository)
        {
            _eventRepository = eventRepository;
            _eventListRepository = eventListRepository;
            _eventListUserRepository = eventListUserRepository;

            EventBus = NullEventBus.Instance;
        }

        public async Task<Event> GetEventAsync(Guid id)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(id);

            if (@event == null)
                throw new UserFriendlyException("Could not found the event, maybe it's deleted!");

            return @event;
        }

        public async Task<EventList> GetListAsync(Guid id)
        {
            var list = await _eventListRepository.FirstOrDefaultAsync(id);

            if (list == null)
                throw new UserFriendlyException("Could not found the list, maybe it's deleted!");

            return list;
        }

        public async Task<Event> CreateEventAsync(Event @event)
        {
            var newEvent = await _eventRepository.InsertAsync(@event);
            return newEvent;
        }

        public async Task<EventList> CreateListAsync(EventList list)
        {
            var newEventList = await _eventListRepository.InsertAsync(list);
            return newEventList;
        }

        public async Task DeleteEventAsync(Event @event)
        {
            await _eventRepository.DeleteAsync(@event);
        }

        public async Task DeleteListAsync(EventList list)
        {
            await _eventListRepository.DeleteAsync(list);
        }

        public async Task UpdateEventAsync(Event @event)
        {
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task UpdateListAsync(EventList list)
        {
            await _eventListRepository.UpdateAsync(list);
        }

        public async Task<EventListUser> AddUserAsync(EventList eventList, User user, SharingType sharingType = SharingType.Client)
        {
            return await _eventListUserRepository.InsertAsync(
                EventListUser.Create(eventList, user, sharingType));
        }

        public async Task RemoveUserAsync(EventList eventList, User user)
        {
            var toDoListUser = await _eventListUserRepository.FirstOrDefaultAsync(
                e => e.EventListId == eventList.Id && e.UserId == user.Id);

            if (toDoListUser == null)
                return;

            await toDoListUser.RemoveAsync(_eventListUserRepository);
        }

        public async Task<IReadOnlyList<User>> GetListUsersAsync(EventList eventList)
        {
            return (await _eventListUserRepository.GetAllListAsync(e => e.EventListId == eventList.Id)).Select(r => r.User).ToList();
        }

        public async Task<IReadOnlyList<EventList>> GetUserListsAsync(User user)
        {
            return (await _eventListUserRepository.GetAllListAsync(e => e.UserId == user.Id)).Select(r => r.EventList).ToList();
        }
        
    }
}
