namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException() { }

    public IncorrectPasswordException(string message) : base(message) { }

}
