using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Organizer.MultiTenancy.Dto;

namespace Organizer.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
