namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;

public class UserCredentialsAlreadyDeletedException : Exception
{
    public UserCredentialsAlreadyDeletedException() { }
    public UserCredentialsAlreadyDeletedException(string message) : base(message) { }
}
