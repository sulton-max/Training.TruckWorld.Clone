using TruckWorld.Domain.Enums;
//using Type = TruckWorld.Domain.Enums.NotificationType;

namespace TruckWorld.Domain.Entities;

/// <summary>
/// represents SmsTemplate class
/// </summary>
public class SmsTemplate : NotificationTemplate
{
    /// <summary>
    /// takes type of Template which was sms
    /// </summary>
    public SmsTemplate()
    {
        Type = NotificationType.Sms;
    }
}
