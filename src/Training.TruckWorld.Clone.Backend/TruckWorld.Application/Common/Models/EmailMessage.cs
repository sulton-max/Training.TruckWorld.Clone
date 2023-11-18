using TruckWorld.Domain.Entities;

namespace TruckWorld.Application.Common.Models;

public class EmailMessage : NotificationMessage
{
    public string SenderEmailAddress { get; set; } = default!;

    public string RecieverEmailAddress { get; set; } = default!;

    public EmailTemplate Template { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;
}