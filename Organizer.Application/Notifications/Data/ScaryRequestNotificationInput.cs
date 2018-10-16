namespace Organizer.Notifications.Data
{
    public class ScaryRequestNotificationInput
    {
        public string SenderUserName { get; set; }

        public string ScaryMessage { get; set; }

        public string TargetUserId { get; set; }
    }
}
