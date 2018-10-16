using Abp.Domain.Entities.Auditing;
using Organizer.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organizer.Notes
{
    [Table("AppNotes")]
    public class Note : FullAuditedEntity<Guid>
    {
        public const int MaxTitleLength = 128;
        public const int MaxContentLength = 2048;
        public const int MaxHashTagLength = 48;

        [Required]
        [MaxLength(MaxTitleLength)]
        public virtual string Title { get; protected set; }

        [MaxLength(MaxContentLength)]
        public virtual string Content { get; protected set; }

        [MaxLength(MaxHashTagLength)]
        public virtual string HashTag { get; protected set; }


        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }


        protected Note() { }

        public static Note Create(string title, long userId, string content = null, string hashTag = null)
        {
            var note = new Note
            {
                Id = Guid.NewGuid(),
                Title = title,
                Content = content,
                HashTag = hashTag
            };

            note.SetUser(userId);

            return note;
        }

        public void ChangeUser(long userId)
        {
            if (UserId == userId)
                return;
            SetUser(userId);
        }

        private void SetUser(long userId)
        {
            UserId = userId;

            // EventBus.Trigger(new NoteUserChangedEvent(this));
        }

        public void Update(string title =null, string content=null, string hashTag=null, long? userId=null)
        {
            Title = title ?? Title;
            Content = content??Content;
            HashTag = hashTag ?? HashTag;
            UserId = userId ?? UserId;
        }
    }
}
