using System.Security.Cryptography;

namespace Training.TruckWorld.Backend.Domain.Exceptions;

public class InvalidEntityException : Exception
{
    public Type Type { get; }
    
    public Guid Id { get; }

    public InvalidEntityException(Type type, Guid id, string message) : base(message)
    {
        Type = type;
        Id = id;
    }
}