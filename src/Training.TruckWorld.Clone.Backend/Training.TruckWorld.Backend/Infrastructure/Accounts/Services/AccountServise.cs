﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;
using Training.TruckWorld.Backend.Persistence.DataContexts;




namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services
{
    internal class AccountServise : IAccountService
    {
        private readonly IDataContext _appDataContext;
        private readonly IUserCredentialsService _userCredentialsService;
        private readonly IValidationService _validationService;
        private readonly UserCredentials _userCredentials;
        private readonly IPasswordHasherService _passwordHasherService;


        
        public AccountServise(IDataContext appDataContext, IUserCredentialsService userCredentialsService, IValidationService validationService, UserCredentials userCredentials, IPasswordHasherService passwordHasherService)
        {
            _appDataContext = appDataContext;
            _userCredentialsService = userCredentialsService;
            _validationService = validationService;
            _userCredentials = userCredentials;
            _passwordHasherService = passwordHasherService;
            
        }

        public async ValueTask<UserCredentials> RegisterUserAsync(string firstName, string lastName, string emailAddress, string password, CancellationToken cancellationToken = default)
        {
            if (_appDataContext.Users.Any(user => user.EmailAddress == emailAddress))
            {
                throw new UserAlreadyExistsException("User with this email address already exists.");
            }

            if (!_validationService.IsValidEmailAddress(emailAddress) || !_validationService.IsValidFullName(firstName) || !_validationService.IsValidFullName(lastName))
            {
                throw new ValidationException("Invalid email address, first name, or last name.");
            }



            var user = new User(firstName, lastName, emailAddress);

            var userCredentials = new UserCredentials(user.Id, password);


            await _userCredentialsService.CreateAsync(userCredentials, cancellationToken: cancellationToken);
            await _appDataContext.Users.AddAsync(user);

          
            return userCredentials;
        }



        public async ValueTask<User> LoginAsync(string emailAddress, string password)
        {
            var user = _appDataContext.Users.FirstOrDefault(user => user.EmailAddress == emailAddress);

            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            var userCredentials = await _userCredentialsService.GetByIdAsync(user.Id);

            if (userCredentials == null || !_passwordHasherService.Verify(password, _userCredentials.Password))
            {
                throw new IncorrectPasswordException("Incorrect password.");
            }

            return user;
        }



        public async ValueTask<User> LogoutDeleteAsync(Guid userId)
        {
            var user = _appDataContext.Users.FirstOrDefault(user => user.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            if (user is ISoftDeletedEntity softDeletedEntity)
            {
                softDeletedEntity.IsDeleted = true;
                softDeletedEntity.DeletedDate = DateTimeOffset.UtcNow;
            }

            return user;
        }
    }

   
    
}


