namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;

public class UserCredentialsAlreadyExistsException : Exception
{
    public UserCredentialsAlreadyExistsException() { }
    public UserCredentialsAlreadyExistsException(string message) : base(message) { }
}
