using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Organizer.Users.Dto;

namespace Organizer.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);
        
        Task<ListResultDto<UserListDto>> GetUsers();

        Task CreateUser(CreateUserInput input);

        Task UpdateUser(UpdateUserInput input);

        Task ChangePassword(ChangePasswordInput input);

        Task<UserDetailOutput> GetDetail();
    }
}