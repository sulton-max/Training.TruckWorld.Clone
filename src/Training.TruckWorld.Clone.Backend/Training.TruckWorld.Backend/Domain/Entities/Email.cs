using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;
#pragma warning disable CS8618

public class Email: SoftDeletedEntity
{
    public string SenderAddress { get; set; }
    
    public string ReceiverAddress { get; set; }
    
    public string Subject { get; set; }
    
    public string Body { get; set; }
    
    public DateTimeOffset SentTime { get; set; }
    
    public bool IsSent { get; set; }
    
    public Email() { }
    public Email(string senderAddress, string receiverAddress, string subject, string body)
    {
        Id = Guid.NewGuid();
        SenderAddress = senderAddress;
        ReceiverAddress = receiverAddress;
        Subject = subject;
        Body = body;
        CreatedDate = DateTime.UtcNow;
    }
}
