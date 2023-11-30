using System.Linq.Expressions;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.Repositories.Interface;

/// <summary>
/// Repository interface for managing smsTemplate-related data operations.
/// </summary>
public interface ISmsTemplateRepository
{
    /// <summary>
    /// Retrieve a collection of smsTemplate based on the specified predicate. 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns>An IQueryable collection of SmsTemplate objects</returns>
    IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate,
        bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Create a collection of smsTemplate based on the specified predicate. 
    /// </summary>
    /// <param name="smsTemplate"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IQueryable collection of SmsTemplate objects</returns>
    ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
}
