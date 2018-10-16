using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Organizer.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using static Organizer.OrganizerConsts;

namespace Organizer.Events
{
    [Table("AppEventListUsers")]
    public class EventListUser : CreationAuditedEntity
    {

        public SharingType SharingType { get; protected set; }

        [ForeignKey("ToDoListId")]
        public virtual EventList EventList { get; protected set; }
        public virtual Guid EventListId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }


        protected EventListUser() { }

        public static EventListUser Create(EventList eventList, User user, SharingType sharingType)
        {
            if (eventList == null)
                throw new ArgumentNullException(nameof(eventList));
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new EventListUser
            {
                EventListId = eventList.Id,
                EventList = eventList,
                UserId = user.Id,
                User = user,
                SharingType = sharingType
            };
        }

        public async Task RemoveAsync(IRepository<EventListUser> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            await repository.DeleteAsync(this);
        }
    }
}
