using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities
{
    public class ContactUser : SoftDeletedEntity
    {
        public Guid UserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public Location Location { get; set; }
        
        public ContactUser() { }
        public ContactUser(Guid userId, string firstName, string lastName, string emailAddress, string phoneNumber, Location location)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Location = location;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
