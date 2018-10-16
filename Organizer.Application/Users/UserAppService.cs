using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Organizer.Authorization;
using Organizer.Users.Dto;
using Microsoft.AspNet.Identity;

namespace Organizer.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize]
    public class UserAppService : OrganizerAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Users)]
        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        [AbpAuthorize(PermissionNames.Pages_Users)]
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        [AbpAuthorize(PermissionNames.Pages_Users)]
        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        [AbpAuthorize(PermissionNames.Pages_Users)]
        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await UserManager.CreateAsync(user));
        }


        public async Task UpdateUser(UpdateUserInput input)
        {
            var user = await GetCurrentUserAsync();

            user.Update(input.Name, input.Surname, input.EmailAddress, input.PhoneNumber, input.Birthday, input.Adress);

            CheckErrors(await UserManager.UpdateAsync(user));
        }


        public async Task ChangePassword(ChangePasswordInput input)
        {
            var user = await GetCurrentUserAsync();

            CheckErrors(await UserManager.ChangePasswordAsync(user.Id, input.OldPassword, input.NewPassword));
        }


        public async Task<UserDetailOutput> GetDetail()
        {
            var user = await GetCurrentUserAsync();

            return user.MapTo<UserDetailOutput>();
        }
    }
}