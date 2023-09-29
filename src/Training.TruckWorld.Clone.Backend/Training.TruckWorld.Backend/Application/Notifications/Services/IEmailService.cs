using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Notifications.Services
{
    public interface IEmailService
    {
        IQueryable<Email> Get(Expression<Func<Email, bool>> expression);
        ValueTask<ICollection<Email>> GetAsync(IEnumerable<Guid> ids);
        ValueTask<Email?> GetByIdAsync(Guid id);
        ValueTask<Email> CreateAsync(Email email, bool saveChanges = true, CancellationToken cancellationToken = default);
        ValueTask<Email> UpdateAsync(Email email, bool saveChanges = true, CancellationToken cancellationToken = default);
        ValueTask<Email> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
        ValueTask<Email> DeleteAsync(Email email, bool saveChanges = true, CancellationToken cancellationToken = default);
    }
}
