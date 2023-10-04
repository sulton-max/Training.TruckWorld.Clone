namespace Training.TruckWorld.Backend.Domain.Exceptions;

public class EntityConflictException : Exception
{
    public Type Type { get; set; }

    public string ParamName { get; set; }


    public EntityConflictException(Type type, string paramName)
        : base($"Entity of type : {type} with this {paramName} is already exist!")
    {
        Type = type;
        ParamName = paramName;
    }
}