using Type = TruckWorld.Domain.Enums.NotificationType;

namespace TruckWorld.Domain.Entities
{
    public class SmsTemplate : NotificationTemplate
    {
        public SmsTemplate()
        {
            Type = Type.Sms;
        }
    }
}
