﻿using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Enums;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class User : SoftDeletedEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public UserRole Role { get; set; }

    public User() { }

    public User(string firstName, string lastName, string emailAddress, UserRole role)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        Role = role;
        CreatedDate = DateTime.UtcNow;
        IsDeleted = false;
    }
}
