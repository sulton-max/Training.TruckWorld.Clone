namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() { }

    public InvalidPasswordException(string message) : base(message) { }
}
