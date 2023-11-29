namespace TruckWorld.Domain.Enums;

/// <summary>
/// Represents a notification event
/// </summary>
public enum NotificationEvent
{
    /// <summary>
    /// Indicates status in rendering the notification
    /// </summary>
    OnRedering,

    /// <summary>
    /// Indicates status in sending the notificaiton
    /// </summary>
    OnSending,

    /// <summary>
    /// Indicates status in saving the notificaiton
    /// </summary>
    OnSaving,
}