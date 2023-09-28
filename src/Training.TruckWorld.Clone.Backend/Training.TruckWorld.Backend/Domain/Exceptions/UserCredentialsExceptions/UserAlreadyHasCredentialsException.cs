namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;

public class UserAlreadyHasCredentialsException : Exception
{
    public UserAlreadyHasCredentialsException() { }

    public UserAlreadyHasCredentialsException(string message) : base(message) { }
}
