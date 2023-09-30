using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Training.TruckWorld.Backend.Domain.Entities;
namespace Training.TruckWorld.Backend.Application.Accounts.Services
{
    public interface IAccountService
    {



        public ValueTask<User> RegisterUserAsync(string firstName, string lastName, string emailAddress, string password, CancellationToken cancellationToken = default);

        public ValueTask<User> LoginAsync(string emailAddress, string password);

    }
}
