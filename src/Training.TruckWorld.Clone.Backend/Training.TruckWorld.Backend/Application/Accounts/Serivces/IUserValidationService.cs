using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.TruckWorld.Backend.Application.Accounts.Serivces
{
    public interface IUserValidationService
    {
        bool IsValidName(string name);

        bool IsValidEmailAddress(string emailAddress);
    }
}
