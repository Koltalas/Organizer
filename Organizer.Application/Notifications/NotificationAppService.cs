using Abp;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Localization;
using Abp.Notifications;
using Abp.UI;
using Organizer.Notifications.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organizer.Notifications
{
    public class NotificationAppService : OrganizerAppServiceBase, INotificationAppService, ITransientDependency
    {
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly INotificationPublisher _notiticationPublisher;
        private readonly IRealTimeNotifier _realTimeNOtifier;
        private readonly IUserNotificationManager _userNotificationManager;

        public NotificationAppService(
            INotificationSubscriptionManager notificationSubscriptionManager,
            INotificationPublisher notiticationPublisher,
            IRealTimeNotifier realTimeNOtifier,
            IUserNotificationManager userNotificationManager)
        {
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _notiticationPublisher = notiticationPublisher;
            _realTimeNOtifier = realTimeNOtifier;
            _userNotificationManager = userNotificationManager;
        }
        public async Task<List<UserNotification>> Publish_ScareUser(ScaryRequestNotificationInput input)
        {

            try
            {
               // throw new UserFriendlyException(input.TargetUserId);

                UserIdentifier ui = new UserIdentifier(1, long.Parse(input.TargetUserId));
           //     await _notificationSubscriptionManager.SubscribeAsync(ui, "VspishkaSparava");


                await _notiticationPublisher.PublishAsync("VspishkaSleva", new ScaryRequestNotificationData(input.SenderUserName, input.ScaryMessage), userIds: new[] { ui });

                //    throw new UserFriendlyException("We still got fucked");

                return await _userNotificationManager.GetUserNotificationsAsync(ui);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

            //  await _realTimeNOtifier.SendNotificationsAsync(new UserNotification())
        }
        public async Task Publish_LowDisk(EntityDto<int> remainingDiskInMb)
        {
            //Example "LowDiskWarningMessage" content for English -> "Attention! Only {remainingDiskInMb} MBs left on the disk!"
            var data = new LocalizableMessageNotificationData(new LocalizableString("LowDiskWarningMessage", "MyLocalizationSourceName"));
            data["remainingDiskInMb"] = remainingDiskInMb.Id;

            await _notiticationPublisher.PublishAsync("System.LowDisk", data, severity: NotificationSeverity.Warn);
        }
    }
}
