﻿namespace Training.TruckWorld.Backend.Domain.Common;

public abstract class AuditableEntity : Entity, IAuditableEntity
{
    public DateTimeOffset CreatedDate { get; set; }
    
    public DateTimeOffset? ModifiedDate { get; set; }
}
