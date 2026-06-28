using FinFlow.Domain.Enums;

namespace FinFlow.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string ActionUrl { get; set; }
        public NotificationPriorities NotificationPriority { get; set; }
        public int UserId { get; set; }
        public int NotificationTypeId { get; set; }
        public User User { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
