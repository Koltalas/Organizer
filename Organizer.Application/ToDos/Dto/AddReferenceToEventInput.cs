using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.ToDos.Dto
{
    public class AddReferenceToEventInput
    {
        [Required]
        public Guid ToDoListId { get; set; }
        
        [Required]
        public Guid EventId { get; set; }
    }
}
