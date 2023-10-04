using Training.TruckWorld.Backend.Domain.Common;

#pragma warning disable CS8618
namespace Training.TruckWorld.Backend.Domain.Entities;

public class EmailTemplate : SoftDeletedEntity
{
    public string Subject { get; set; }

    public string Body { get; set; }

    public EmailTemplate()
    {
    }

    public EmailTemplate(string subject, string body)
    {
        Subject = subject;
        Body = body;
    }
}