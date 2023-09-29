namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;

public class ValidationException : Exception
{
    public ValidationException() { }

    public ValidationException(string message) : base(message) { }
}
