using TruckWorld.Domain.Common;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Domain.Entities;

/// <summary>
/// represents NotificationTemplate abstract class
/// </summary>
public abstract class NotificationTemplate : IEntity
{
    /// <summary>
    /// gets or sets id of IEntity
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// gets or sets type of NotificationType
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// gets or sets TemplateType of NotificationTemplateType
    /// </summary>
    public NotificationTemplateType TemplateType { get; set; }

    /// <summary>
    /// gets or sets content of NotificationTemplate it can not be default
    /// </summary>
    public string Content { get; set; } = default!;
}
