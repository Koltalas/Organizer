using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Organizer.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using static Organizer.OrganizerConsts;

namespace Organizer.ToDos
{
    [Table("AppToDoListUsers")]
    public class ToDoListUser : CreationAuditedEntity
    {

        public SharingType SharingType { get; protected set; }

        [ForeignKey("ToDoListId")]
        public virtual ToDoList ToDoList { get; protected set; }
        public virtual Guid ToDoListId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }


        protected ToDoListUser() { }

        public static ToDoListUser Create(ToDoList toDoList, User user, SharingType sharingType)
        {
            if (toDoList == null)
                throw new ArgumentNullException("toDoList");
            if (user == null)
                throw new ArgumentNullException("user");

            return new ToDoListUser
            {
                ToDoListId = toDoList.Id,
                ToDoList = toDoList,
                UserId = user.Id,
                User = user,
                SharingType = sharingType
            };
        }

        public async Task RemoveAsync(IRepository<ToDoListUser> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            await repository.DeleteAsync(this);
        }
    }
}
