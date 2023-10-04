namespace Training.TruckWorld.Backend.Domain.Exceptions;

public class EntityDeletedException : Exception
{
    public Type Type { get; }
    public Guid? Id { get; }

    public EntityDeletedException(Type type, Guid? id = null)
        : base(id is not null
            ? $"Entity of type : {type} with ID {id} is already deleted!"
            : $"Entity of type : {type} already deleted!")
    {
        Type = type;
        Id = id;
    }
}