using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Events.Dto
{
    public class CreateEventInListInput
    {
        [Required]
        [StringLength(Event.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Event.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        [Required]
        public bool AllDay { get; set; }


        public virtual Guid EventListId { get; set; }
    }

    public class CreateEventInput : CreateEventInListInput
    {
        [Required]
        public override Guid EventListId { get; set; }
    }
}
