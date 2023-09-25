using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.TruckWorld.Backend.Application.Accounts.Serivces
{
    public interface IUserValidationService
    {
        bool IsValidName(string fullName);

        bool IsValidEmailAddress(string emailAddress);

        bool IsValidPassword(string password);
    }
}
