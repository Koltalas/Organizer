using System.Threading.Tasks;
using Abp.Application.Services;
using Organizer.Sessions.Dto;

namespace Organizer.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
