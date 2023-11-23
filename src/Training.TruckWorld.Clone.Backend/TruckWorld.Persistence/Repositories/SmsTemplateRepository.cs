using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TruckWorld.Domain.Entities;
using TruckWorld.Persistence.DataContext;
using TruckWorld.Persistence.Repositories.Interfaces;

namespace TruckWorld.Persistence.Repositories
{
    public class SmsTemplateRepository : EntityRepositoryBase<SmsTemplate, NotificationsDbContext>,
        ISmsTemplateRepository
    {
        public SmsTemplateRepository(NotificationsDbContext dbContext) : base(dbContext)
        {

        }

        ValueTask<SmsTemplate> ISmsTemplateRepository.CreateAsync(
            SmsTemplate smsTemplate, 
            bool saveChanges,
            CancellationToken cancellationToken
            ) =>
                base.CreateAsync(smsTemplate, saveChanges, cancellationToken);
        

        IQueryable<SmsTemplate> ISmsTemplateRepository.Get(
            Expression<Func<SmsTemplate, bool>>? predicate, 
            bool asNoTracking
            ) =>
                base.Get(predicate, asNoTracking);
       
    }
}
