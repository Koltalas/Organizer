using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.ToDos.Dto
{
    public class CreateElemInListInput
    {
        [Required]
        [StringLength(ToDoElem.MaxTitleLength)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }
        
        public virtual Guid ToDoListId { get; set; }
    }

    public class CreateElemInput : CreateElemInListInput
    {
        [Required]
        public override Guid ToDoListId { get; set; }
    }
}
