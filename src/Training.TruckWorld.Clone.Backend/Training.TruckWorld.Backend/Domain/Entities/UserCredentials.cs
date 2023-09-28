using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities
{
    public class UserCredentials: SoftDeletedEntity
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public UserCredentials() { }
        public UserCredentials(Guid userId, string password) 
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Password = password; 
            CreatedDate = DateTime.UtcNow;
        }
    }
}
