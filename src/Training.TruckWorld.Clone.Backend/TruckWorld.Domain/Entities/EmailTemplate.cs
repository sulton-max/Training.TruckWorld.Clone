using Type = TruckWorld.Domain.Enums.NotificationType;


namespace TruckWorld.Domain.Entities
{
    public class EmailTemplate : NotificationTemplate
    {
        public string Subject { get; set; } = default!;
        public EmailTemplate()
        {
            Type = Type.Email;
        }
    }
}
