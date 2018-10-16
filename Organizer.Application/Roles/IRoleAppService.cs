using System.Threading.Tasks;
using Abp.Application.Services;
using Organizer.Roles.Dto;

namespace Organizer.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
