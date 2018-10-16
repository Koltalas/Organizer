using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Events.Dto
{
    public class GetAccessToListInput
    {
        [StringLength(EventList.MaxSharingKeyLength)]
        public  string SharingKey { get; set; }

        [StringLength(EventList.MaxSharingPasswordLength)]
        public  string SharingPassword { get; set; }
    }
}
