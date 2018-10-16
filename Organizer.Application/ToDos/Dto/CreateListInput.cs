using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Organizer.ToDos.Dto
{
    public class CreateListInput
    {
        [Required]
        [StringLength(ToDoList.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(ToDoList.MaxDescriptionLength)]
        public string Description { get; set; }

        public List<CreateElemInListInput> Elems { get; set; }
    }
}
