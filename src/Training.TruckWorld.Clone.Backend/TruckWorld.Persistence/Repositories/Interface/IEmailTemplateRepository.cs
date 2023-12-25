using System.Linq.Expressions;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.Repositories.Interface;

/// <summary>
/// Repository interface for managing email-related data operations.
/// </summary>
public interface IEmailTemplateRepository
{
    /// <summary>
    /// Retrieves a collection of templates based on the specified predicate. 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns>An IQueryable collection of emailTemplate objects</returns>
    IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Create a collection of emailTemplate based on the specified predicate. 
    /// </summary>
    /// <param name="emailTemplate"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IQueryable collection of SmsTemplate objects</returns>
    ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);
}
