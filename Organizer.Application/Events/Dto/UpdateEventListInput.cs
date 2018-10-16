using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Events.Dto
{
    public class UpdateEventListInput
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(EventList.MaxTitleLength)]
        public string Title { get; set; } = null;

        [StringLength(EventList.MaxDescriptionLength)]
        public string Description { get; set; } = null;

        [StringLength(EventList.MaxColorLength)]
        public string Color { get; set; } = null;

        public long? UserId { get; set; } = null;

    }
}
