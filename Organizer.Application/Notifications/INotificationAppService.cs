using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Organizer.Notifications.Data;
using Abp.Notifications;
using Abp.Application.Services.Dto;

namespace Organizer.Notifications
{
    public interface INotificationAppService : IApplicationService
    {
        Task<List<UserNotification>> Publish_ScareUser(ScaryRequestNotificationInput input);
        Task Publish_LowDisk(EntityDto<int> remainingDiskInMb);
    }
}
