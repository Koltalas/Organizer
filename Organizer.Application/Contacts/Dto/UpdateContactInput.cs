using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Contacts.Dto
{
    public class UpdateContactInput
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(Contact.MaxFNameLength)]
        public string FName { get; set; } = null;

        [StringLength(Contact.MaxLNameLegnth)]
        public string LName { get; set; } = null;

        [DataType(DataType.EmailAddress)]
        [StringLength(Contact.MaxEmailLength)]
        public string Email { get; set; } = null;

        [DataType(DataType.PhoneNumber)]
        [StringLength(Contact.MaxPhoneNumberLegth)]
        public string PhoneNumber { get; set; } = null;

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; } = null;

        [StringLength(Contact.MaxAdressLength)]
        public string Adress { get; set; } = null;


        public long? ProfileId { get; set; } = null;

        public long? UserId { get; set; } = null;
    }
}
