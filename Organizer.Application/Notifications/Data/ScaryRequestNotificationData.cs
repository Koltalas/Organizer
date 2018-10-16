using Abp.Notifications;
using System;

namespace Organizer.Notifications.Data
{
    [Serializable]
    public class ScaryRequestNotificationData : NotificationData
    {
        public string SenderUserName { get; set; }

        public string ScaryMessage { get; set; }

        public ScaryRequestNotificationData(string senderUserName, string scaryMessage)
        {
            SenderUserName = senderUserName;
            ScaryMessage = scaryMessage;
        }
    }
}
