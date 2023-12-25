using System.Linq.Expressions;
using TruckWorld.Domain.Entities;
using TruckWorld.Persistence.DataContext;
using TruckWorld.Persistence.Repositories.Interface;

namespace TruckWorld.Persistence.Repositories
{
    public class EmailTemplateRepository : EntityRepositoryBase<EmailTemplate, NotificationsDbContext>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(NotificationsDbContext dbContext) : base(dbContext)
        {

        }

        ValueTask<EmailTemplate> IEmailTemplateRepository.CreateAsync(
            EmailTemplate emailTemplate,
            bool asNoTracking,
            CancellationToken cancellationToken
            ) =>
                base.CreateAsync(emailTemplate, asNoTracking, cancellationToken);


        IQueryable<EmailTemplate> IEmailTemplateRepository.Get(
            Expression<Func<EmailTemplate, bool>>? predicate,
            bool asNoTracking
            ) =>
                base.Get(predicate, asNoTracking);
    }
}
