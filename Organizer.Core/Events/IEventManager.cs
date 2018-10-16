using Abp.Domain.Services;
using Organizer.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Organizer.OrganizerConsts;

namespace Organizer.Events
{
    public interface IEventManager : IDomainService
    {
        Task<Event> GetEventAsync(Guid id);
        Task<EventList> GetListAsync(Guid id);
        Task<Event> CreateEventAsync(Event @event);
        Task<EventList> CreateListAsync(EventList list);
        Task DeleteEventAsync(Event @event);
        Task DeleteListAsync(EventList list);
        Task UpdateEventAsync(Event @event);
        Task UpdateListAsync(EventList list);
        Task<EventListUser> AddUserAsync(EventList eventList, User user, SharingType sharingType = SharingType.Client);
        Task RemoveUserAsync(EventList eventList, User user);
        Task<IReadOnlyList<User>> GetListUsersAsync(EventList eventList);
        Task<IReadOnlyList<EventList>> GetUserListsAsync(User user);
    }
}
