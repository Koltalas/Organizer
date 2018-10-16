using System.ComponentModel.DataAnnotations;

namespace Organizer.Notes.Dto
{
    public class CreateNoteInput
    {
        [Required]
        [StringLength(Note.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Note.MaxContentLength)]
        public string Content { get; set; }

        [StringLength(Note.MaxHashTagLength)]
        public string HashTag{ get; set; }

        
    }
}
