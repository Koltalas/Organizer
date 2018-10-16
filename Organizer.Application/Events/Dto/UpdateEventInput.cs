using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Events.Dto
{
    public class UpdateEventInput
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(Event.MaxTitleLength)]
        public string Title { get; set; } = null;

        [StringLength(Event.MaxDescriptionLength)]
        public string Description { get; set; } = null;

        public DateTime? Start { get; set; } = null;

        public DateTime? End { get; set; } = null;

        public bool? AllDay { get; set; } = null;

        public Guid? ListId { get; set; } = null;

    }
}

