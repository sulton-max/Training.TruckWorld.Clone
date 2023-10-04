using Training.TruckWorld.Backend.Domain.Common;

#pragma warning disable CS8618
namespace Training.TruckWorld.Backend.Domain.Entities;

public class UserCredentials : SoftDeletedEntity
{
    public Guid UserId { get; set; }

    public string Password { get; set; }

    public UserCredentials()
    {
    }

    public UserCredentials(Guid userId, string password)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Password = password;
        CreatedDate = DateTime.UtcNow;
    }
}