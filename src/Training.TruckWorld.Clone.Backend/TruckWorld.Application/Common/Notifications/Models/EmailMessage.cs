using TruckWorld.Domain.Entities;

namespace TruckWorld.Application.Common.Notifications.Models;

/// <summary>
/// Represents a email message
/// </summary>
public class EmailMessage : NotificationMessage
{
    /// <summary>
    /// Gets or sets senders' the email address
    /// </summary>
    public string SenderEmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the recievers' email address
    /// </summary>
    public string RecieverEmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets EmailTemplate entities
    /// </summary>
    public EmailTemplate Template { get; set; } = default!;

    /// <summary>
    /// Gets or sets subject of the messages
    /// </summary>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// Gets or sets body of the message
    /// </summary>
    public string Body { get; set; } = default!;
}