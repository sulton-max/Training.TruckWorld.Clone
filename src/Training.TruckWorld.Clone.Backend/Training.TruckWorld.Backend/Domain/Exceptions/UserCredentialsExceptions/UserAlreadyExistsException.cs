using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() { }

        public UserAlreadyExistsException(string message) : base(message) { }

    }
}
