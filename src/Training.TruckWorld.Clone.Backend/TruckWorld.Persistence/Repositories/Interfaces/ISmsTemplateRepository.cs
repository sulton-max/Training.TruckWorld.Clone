using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.Repositories.Interfaces
{
    public interface ISmsTemplateRepository
    {
        IQueryable<SmsTemplate> Get(Expression<Func<SmsTemplate, bool>>? predicate = default, bool asNoTracking = false);

        ValueTask<SmsTemplate> CreateAsync(
            SmsTemplate smsTemplate,
            bool saveChanges = true,
            CancellationToken cancellationToken = default);
    }
}
