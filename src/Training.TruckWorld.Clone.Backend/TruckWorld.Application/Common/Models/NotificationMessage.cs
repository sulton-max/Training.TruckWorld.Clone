namespace TruckWorld.Application.Common.Models;

public class NotificationMessage
{
    public Guid SenderId { get; set; }

    public Guid RecieverId { get; set; }

    public Dictionary<string, string> Variables { get; set; }

    public bool IsSuccessfull { get; set; }

    public string? ErrorMessage { get; set; }
}