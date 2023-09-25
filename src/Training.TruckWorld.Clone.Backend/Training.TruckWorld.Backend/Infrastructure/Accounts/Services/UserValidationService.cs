using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Application.Accounts.Serivces;
using Training.TruckWorld.Backend.Domain.Entities;

//Implement UserValidationService
//Description
//Validation service checks if informations of user is valid or not, and returns true or false

//Requirements
//-Validate informations of user for CRUD
//-Shoud let user know about invalid informations
//-Must check if email of user is unique(it means user cannot register with the email that's already used)

//Deliverables
//-service that validate User model

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services
{
    public class UserValidationService : IUserValidationService
    {
        public bool IsValidEmailAddress(string emailAddress)
        {
            if (!string.IsNullOrWhiteSpace(emailAddress))
                return Regex.IsMatch(emailAddress, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return false;
        }

        public bool IsValidName(string name)
        {
            if (name != null)
                return Regex.IsMatch(name, @"^[A-Za-z]+ [A-Za-z]+$");
            return false;
        }

        public bool IsValidPassword(string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
                return Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$");
            return false;
        }
    }
}


