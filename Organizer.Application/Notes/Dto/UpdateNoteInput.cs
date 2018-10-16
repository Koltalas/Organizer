using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Notes.Dto
{
    public class UpdateNoteInput
    {
        [Required]
        public Guid Id { get; set; }
        
        [StringLength(Note.MaxTitleLength)]
        public string Title { get; set; } = null;

        [StringLength(Note.MaxContentLength)]
        public string Content { get; set; } = null;

        [StringLength(Note.MaxHashTagLength)]
        public string HashTag { get; set; } = null;


        public long? UserId { get; set; } = null;

    }
}
