

namespace TruckWorld.Domain.Common;

public class SoftDeletedEntity : AuditableEntity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }
}
