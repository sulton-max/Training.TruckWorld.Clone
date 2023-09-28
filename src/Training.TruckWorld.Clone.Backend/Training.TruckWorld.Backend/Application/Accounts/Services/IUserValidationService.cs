namespace Training.TruckWorld.Backend.Application.Accounts.Services
{
    public interface IUserValidationService
    {
        public bool IsValidName(string name);
        public bool IsValidPassword(string password);
        public bool IsValidEmail(string email);
    }
}
