namespace Training.TruckWorld.Backend.Domain.Exceptions;

public class EntityDeletedException : Exception
{
    public Type Type { get; }
    public Guid Id { get; }

    public EntityDeletedException(Type type, Guid id)
        : base($"Entity of type : {type} with ID {id} is already deleted!")
    {
        Type = type;
        Id = id;
    }
}