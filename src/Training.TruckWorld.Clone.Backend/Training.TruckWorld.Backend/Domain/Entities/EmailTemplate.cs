using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class EmailTemplate : SoftDeletedEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public EmailTemplate() { }
    public EmailTemplate(string subject, string body)
    {
        Id = Guid.NewGuid();
        Subject = subject;
        Body = body;
        CreatedDate = DateTime.UtcNow;
    }
}
