using Abp.Domain.Entities.Auditing;
using Organizer.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organizer.Contacts
{
    [Table("AppContacts")]
    public class Contact : FullAuditedEntity<Guid>
    {
        public const int MaxFNameLength = 128;
        public const int MaxLNameLegnth = 128;
        public const int MaxAdressLength = 128;
        public const int MaxEmailLength = 128;
        public const int MaxPhoneNumberLegth = 20;


        [Required]
        [StringLength(MaxFNameLength)]
        public virtual string FName { get; protected set; }

        [StringLength(MaxLNameLegnth)]
        public virtual string LName { get; protected set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(MaxEmailLength)]
        public virtual string Email { get; protected set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(MaxPhoneNumberLegth)]
        public virtual string PhoneNumber { get; protected set; }

        [DataType(DataType.Date)]
        public virtual DateTime? Birthday { get; protected set; }

        [StringLength(MaxAdressLength)]
        public virtual string Adress { get; protected set; }


        [ForeignKey("ProfileId")]
        public virtual User Profile { get; protected set; }
        public virtual long? ProfileId { get; protected set; }


        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }




        public string FullName => FName + " " + LName;

        public bool IsRealUser => ProfileId.HasValue;

        public string ProfileUserName => IsRealUser ? Profile.UserName : null;


        protected Contact() { }


        public static Contact Create(string fName, long userId, string lName = null, string email = null,
            string phoneNumber = null, DateTime? birthday = null, string adress = null, long? profileId = null)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                FName = fName,
                LName = lName,
                Email = email,
                PhoneNumber = phoneNumber,
                Birthday = birthday,
                Adress = adress
            };

            contact.SetUser(userId);
            if (profileId != null)
                contact.SetProfile(profileId.Value);

            return contact;
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

            // EventBus.Trigger(new ContactUserChangedEvent(this));
        }
        public void ChangeProfile(long profileId)
        {
            if (ProfileId == profileId)
                return;
            SetProfile(profileId);
        }

        private void SetProfile(long profileId)
        {
            ProfileId = profileId;

            // EventBus.Trigger(new ContactProfileUserChangedEvent(this));
        }



        public void Update(string fName = null, string lName = null, string email = null,
            string phoneNumber = null, DateTime? birthday = null, string adress = null, long? profileId = null, long? userId = null)
        {
            FName = fName ?? FName;
            LName = lName ?? LName;
            Email = email ?? Email;
            PhoneNumber = phoneNumber ?? PhoneNumber;
            Birthday = birthday ?? Birthday;
            Adress = adress ?? Adress;
            ProfileId = profileId ?? ProfileId;
            UserId = userId ?? UserId;
        }

    }
}
