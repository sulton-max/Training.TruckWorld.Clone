namespace Training.TruckWorld.Backend.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public Type Type { get; set; }

    public Guid? Id { get; set; }

    public EntityNotFoundException(Type type, Guid? id = null)
        : base(id is not null
            ? $"Entity of type : {type} with ID {id} not found!"
            : $"Entity of type : {type} not found!")
    {
        Type = type;
        Id = id;
    }
}