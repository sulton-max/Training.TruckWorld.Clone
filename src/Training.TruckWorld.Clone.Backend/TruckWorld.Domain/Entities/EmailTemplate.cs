using Type = TruckWorld.Domain.Enums.NotificationType;

namespace TruckWorld.Domain.Entities;

/// <summary>
/// represents emailTemplate
/// </summary>
public class EmailTemplate : NotificationTemplate
{
    /// <summary>
    /// gets or sets the subject of Email
    /// </summary>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// takes notificationType from Type which was email
    /// </summary>
    public EmailTemplate()
    {
        Type = Type.Email;
    }
}