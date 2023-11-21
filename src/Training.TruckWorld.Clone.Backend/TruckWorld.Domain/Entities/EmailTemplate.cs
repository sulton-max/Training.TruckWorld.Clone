using TruckWorld.Domain.Common.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Domain.Entities;

public class EmailTemplate : IEntity
{
    public EmailTemplate() => Type = NotificationType.Email;

    public Guid Id { get; set; }

    public NotificationType Type { get; set; }
}