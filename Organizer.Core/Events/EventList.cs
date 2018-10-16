using Abp.Domain.Entities.Auditing;
using Abp.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Organizer.Events
{
    [Table("AppEventLists")]
    public class EventList : FullAuditedEntity<Guid>
    {
        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;
        public const int MaxColorLength = 7;
        public const int MaxSharingKeyLength = 16;
        public const int MaxSharingPasswordLength = 68;


        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Title { get; protected set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; protected set; }


        [StringLength(MaxColorLength)]
        public virtual string Color { get; protected set; }




        [StringLength(MaxSharingKeyLength)]
        public virtual string SharingKey { get; protected set; }

        [StringLength(MaxSharingPasswordLength)]
        public virtual string SharingPassword { get; protected set; }



        [ForeignKey("EventListId")]
        public virtual ICollection<Event> Events { get; protected set; }

        [ForeignKey("EventListId")]
        public virtual ICollection<EventListUser> ListUsers { get; protected set; }


        private bool _owner = true;
        public bool Owner => _owner;

        public EventList IsOwner(long? userId)
        {
            _owner = CreatorUserId == userId;
            return this;
        }


        public bool IsShared => !SharingKey.IsNullOrWhiteSpace();




        protected EventList() { }



        public static EventList Create(string title, string description = null, string color = null)
        {
            var eventList = new EventList
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Color = color
            };


            eventList.Events = new Collection<Event>();
            eventList.ListUsers = new Collection<EventListUser>();


            return eventList;
        }

        public void Add(Event @event)
        {
            Events.Add(@event);
        }

        public void Update(string title = null, string description = null, string hexColor = null)
        {
            Title = title ?? Title;
            Description = description ?? Description;
            Color = hexColor ?? Color;
        }


        public string GenerateSharing(string sharingPassword)
        {
            SharingPassword = new PasswordHasher().HashPassword(sharingPassword);
            return SharingKey = GenereateRandomKey;
        }

        public void ResetPassword(string newPassword)
        {
            SharingPassword = new PasswordHasher().HashPassword(newPassword);
        }

        public bool CheckPassword(string password)
        {
            return SharingPassword.Equals(new PasswordHasher().HashPassword(password));
        }

        private string GenereateRandomKey => Guid.NewGuid().ToString("N").Truncate(8);

    }
}
