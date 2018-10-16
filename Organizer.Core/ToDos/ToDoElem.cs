using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Organizer.Events;

namespace Organizer.ToDos
{
    [Table("AppToDoElems")]
    public class ToDoElem : FullAuditedEntity<Guid>
    {
        public const int MaxTitleLength = 128;

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Title { get; protected set; }

        public virtual bool IsCompleted { get; protected set; }


        [ForeignKey("ToDoListId")]
        public virtual ToDoList ToDoList { get; protected set; }
        public virtual Guid ToDoListId { get; protected set; }

        protected ToDoElem() { }


        public static ToDoElem Create(string title, Guid toDoListId, bool isCompleted = false)
        {
            var toDoElem = new ToDoElem
            {
                Id = Guid.NewGuid(),
                Title = title,
                ToDoListId = toDoListId,
                IsCompleted = isCompleted
            };


            return toDoElem;
        }

        public async Task Remove(IRepository<ToDoElem, Guid> repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            await repository.DeleteAsync(this);
        }

        public void Update(string title = null, bool? isCompleted = null, Guid? listId = null)
        {
            Title = title ?? Title;
            IsCompleted = isCompleted ?? IsCompleted;
            ToDoListId = listId ?? ToDoListId;
        }

    }
}
