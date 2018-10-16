using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Contacts.Dto
{
    public class CreateContactInput
    {
        [Required]
        [StringLength(Contact.MaxFNameLength)]
        public  string FName { get;  set; }

        [StringLength(Contact.MaxLNameLegnth)]
        public  string LName { get;  set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(Contact.MaxEmailLength)]
        public  string Email { get;  set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(Contact.MaxPhoneNumberLegth)]
        public  string PhoneNumber { get;  set; }

        [DataType(DataType.Date)]
        public  DateTime? Birthday { get;  set; }

        [StringLength(Contact.MaxAdressLength)]
        public  string Adress { get;  set; }
        
        
        public string ProfileUserName { get;  set; }
    }
}
