using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;

namespace Organizer.Contacts.Dto
{
    public class LinkUserProfileInput
    {
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        public Guid ContactId { get; set; }
    }
}
