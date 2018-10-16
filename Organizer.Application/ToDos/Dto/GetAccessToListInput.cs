using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.ToDos.Dto
{
    public class GetAccessToListInput
    {
        [StringLength(ToDoList.MaxSharingKeyLength)]
        public string SharingKey { get; set; }

        [StringLength(ToDoList.MaxSharingPasswordLength)]
        public string SharingPassword { get; set; }
    }
}
