using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;

namespace Organizer.Users.Dto
{
    public class UpdateUserInput
    {
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; } = null;

        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; } = null;

        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; } = null;

        [DataType(DataType.PhoneNumber)]
        [StringLength(User.MaxPhoneNumberLegth)]
        public string PhoneNumber { get; set; } = null;

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; } = null;

        [StringLength(User.MaxAdressLength)]
        public string Adress { get; set; } = null;

    }
}
