namespace Training.TruckWorld.Backend.Domain.Exceptions;

public class ExistingEntityException : Exception
{
    public Type Type { get; set; }
    public Guid? Id { get; set; }

    public ExistingEntityException(Type type, Guid? id = default)
        : base($"Entity of type : {type} with ID {id} is already exist!")
    {
        Type = type;
        Id = id;
    }
}