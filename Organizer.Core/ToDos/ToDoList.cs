using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNet.Identity;
using Organizer.Events;

namespace Organizer.ToDos
{
    [Table("AppToDoLists")]
    public class ToDoList : FullAuditedEntity<Guid>
    {
        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;
        public const int MaxSharingKeyLength = 16;
        public const int MaxSharingPasswordLength = 68;

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Title { get; protected set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; protected set; }


        [StringLength(MaxSharingKeyLength)]
        public virtual string SharingKey { get; protected set; }

        [StringLength(MaxSharingPasswordLength)]
        public virtual string SharingPassword { get; protected set; }


        [ForeignKey("EventId")]
        public virtual Event Event { get; protected set; }
        public virtual Guid? EventId { get; protected set; }



        [ForeignKey("ToDoListId")]
        public virtual ICollection<ToDoElem> Elems { get; protected set; }

        [ForeignKey("ToDoListId")]
        public virtual ICollection<ToDoListUser> ListUsers { get; protected set; }


        public string EventTitle => Event?.Title;

        public bool IsShared => !SharingKey.IsNullOrWhiteSpace();

        protected ToDoList() { }

        public static ToDoList Create(string title, string description = null)
        {
            var toDoList = new ToDoList
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description
            };


            toDoList.Elems = new Collection<ToDoElem>();
            toDoList.ListUsers = new Collection<ToDoListUser>();


            return toDoList;
        }



        public void Add(ToDoElem elem)
        {
            Elems.Add(elem);
        }

        public void Update(string title = null, string description = null)
        {
            Title = title ?? Title;
            Description = description ?? Description;
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


        public void AddReferenceToEvent(Guid eventId)
        {
            EventId = eventId;
        }

    }
}