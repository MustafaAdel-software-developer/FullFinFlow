namespace FinFlow.Domain.Entities
{
    public class NotificationType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
