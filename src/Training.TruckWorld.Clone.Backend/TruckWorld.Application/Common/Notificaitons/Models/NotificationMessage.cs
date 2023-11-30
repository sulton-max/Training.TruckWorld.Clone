namespace TruckWorld.Application.Common.Notificaitons.Models;

/// <summary>
/// Represent a notification message
/// </summary>
public class NotificationMessage
{
    /// <summary>
    /// Gets or sets sender's ids
    /// </summary>
    public Guid SenderId { get; set; }

    /// <summary>
    /// Gets or sets reciever's ids
    /// </summary>
    public Guid RecieverId { get; set; }

    /// <summary>
    /// Gets or sets variables
    /// </summary>
    public Dictionary<string, string> Variables { get; set; }

    /// <summary>
    /// Gets or sets that notification is sent or not 
    /// </summary>
    public bool IsSuccessfull { get; set; }

    /// <summary>
    /// Gets or sets error message while was sending message
    /// </summary>
    public string? ErrorMessage { get; set; }
}