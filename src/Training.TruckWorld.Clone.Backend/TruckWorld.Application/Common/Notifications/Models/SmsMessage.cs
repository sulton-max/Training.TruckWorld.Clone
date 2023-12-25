using TruckWorld.Domain.Entities;

namespace TruckWorld.Application.Common.Notifications.Models;

/// <summary>
/// Represents a sms message
/// </summary>
public class SmsMessage : NotificationMessage
{
    /// <summary>
    /// Gets or sets senders' phone numbers
    /// </summary>
    public string SenderPhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets recievers' phone numbers
    /// </summary>
    public string RecieverPhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets SmsTemplate entities
    /// </summary>
    public SmsTemplate Template { get; set; } = default!;

    /// <summary>
    /// Gets or sets the sms messages
    /// </summary>
    public string Message { get; set; } = default!;
}