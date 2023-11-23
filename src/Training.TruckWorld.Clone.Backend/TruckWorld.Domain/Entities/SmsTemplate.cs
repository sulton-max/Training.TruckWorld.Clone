using Type = TruckWorld.Domain.Enums.NotificationType;

namespace TruckWorld.Domain.Entities;

/// <summary>
/// SmsTemplate model implented NotificationTemplate abstract model
/// </summary>
public class SmsTemplate : NotificationTemplate
{
    public SmsTemplate()
    {
        Type = Type.Sms;
    }
}
