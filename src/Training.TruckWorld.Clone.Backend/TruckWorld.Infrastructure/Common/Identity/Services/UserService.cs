﻿using System.Linq.Expressions;
using FluentValidation;
using TruckWorld.Application.Common.Identity.Services;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;
using TruckWorld.Infrastructure.Common.Validators;
using TruckWorld.Persistence.Repositories.Interface;

namespace TruckWorld.Infrastructure.Common.Identity.Services;

public class UserService (IUserRepository userRepository,
   UserValidator userValidate) : IUserService
{
    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = null,
        bool asNoTracking = false
        )
    {
        return userRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        var validationResult = userValidate.Validate(user, options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(
        Guid userId,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.DeleteByIdAsync(userId, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.DeleteAsync(user, saveChanges, cancellationToken);
    }
}