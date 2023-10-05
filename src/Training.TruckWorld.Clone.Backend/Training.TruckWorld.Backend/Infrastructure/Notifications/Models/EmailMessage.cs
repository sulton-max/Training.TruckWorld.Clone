namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Models;

public class EmailMessage
{
    public string SenderAddress { get; set; }

    public string ReceiverAddress { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public DateTimeOffset SentTime { get; set; }

    public bool IsSent { get; set; }

    public EmailMessage()
    {
    }

    public EmailMessage(string senderAddress, string receiverAddress, string subject, string body)
    {
        SenderAddress = senderAddress;
        ReceiverAddress = receiverAddress;
        Subject = subject;
        Body = body;
    }
}