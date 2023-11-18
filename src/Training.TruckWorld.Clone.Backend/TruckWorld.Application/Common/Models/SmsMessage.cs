using TruckWorld.Domain.Entities;

namespace TruckWorld.Application.Common.Models;

public class SmsMessage : NotificationMessage
{
    public string SenderPhoneNumber { get; set; } = default!;

    public string RecieverPhoneNumber { get; set; } = default!;

    public SmsTemplate Template { get; set; } = default!;

    public string Message { get; set; } = default!;
}