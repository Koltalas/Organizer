using Abp.Authorization;
using Organizer.Authorization.Roles;
using Organizer.MultiTenancy;
using Organizer.Users;

namespace Organizer.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
