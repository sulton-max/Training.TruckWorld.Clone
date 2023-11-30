using TruckWorld.Domain.Common;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Domain.Entities;

public class SmsTemplate : IEntity
{
    public SmsTemplate() => Type = NotificationType.Sms;

    public Guid Id { get; set; }

    public NotificationType Type { get; set; }
}