using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.Repositories.Interfaces
{
    public interface IEmailTemplateRepository
    {
        IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default, bool asNoTracking = false);

        ValueTask<EmailTemplate> CreateAsync(
            EmailTemplate emailTemplate,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);
    }
}
