namespace TruckWorld.Domain.Enums;

/// <summary>
/// Represents a notification event
/// </summary>
public enum NotificationEvent
{
    /// <summary>
    /// status in rendering the notification
    /// </summary>
    OnRedering,

    /// <summary>
    /// status in sending the notificaiton
    /// </summary>
    OnSending,

    /// <summary>
    /// status in saving the notificaiton
    /// </summary>
    OnSaving,
}