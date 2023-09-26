using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Application.Accounts.Services;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services
{
    public class AccauntServise
    {
        private readonly UserValidationService _userValidationService;
        
        public AccauntServise(UserValidationService validationService)
        {
            _userValidationService = validationService;
        }

        public AccauntServise() { }


        public bool IUpdateEmail(string email)
        {
            if (!_userValidationService.IsValidEmail(email))
                return false;
            return true;
        }
        public bool IUpdateFullName(string firsname, string lastname)
        {
            if (!_userValidationService.IsValidName(firsname, lastname))
                return false;
            return true;
        }

        public bool IUpdatePassword(string password)
        {
            if (!_userValidationService.IsValidPassword(password))
                return false;
            return true;
        }
    }
}
