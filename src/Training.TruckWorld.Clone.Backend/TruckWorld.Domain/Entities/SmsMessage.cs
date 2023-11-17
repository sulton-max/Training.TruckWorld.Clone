using TruckWorld.Domain.Common.Entities;

namespace TruckWorld.Domain.Entities;

public class SmsMessage : IEntity
{
    public Guid Id { get; set; }

}