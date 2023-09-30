namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {

    }

    public UserNotFoundException(string message) : base(message) { }
}
