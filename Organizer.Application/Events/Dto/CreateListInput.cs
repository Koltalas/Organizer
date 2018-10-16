using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Events.Dto
{
    public class CreateListInput
    {
        [Required]
        [StringLength(EventList.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(EventList.MaxDescriptionLength)]
        public string Description { get; set; }

        [StringLength(EventList.MaxColorLength)]
        public string Color { get; set; }

        public List<CreateEventInListInput> Events { get; set; }
    }
}
