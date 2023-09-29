using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Services
{
    public class EmailService : IEmailService
    {
        private readonly IDataContext _appDataContext;

        public EmailService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<Email> CreateAsync(Email email, bool saveChanges = true)
        {
            await _appDataContext.Emails.AddAsync(email);

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();

            return email;
        }

        public async ValueTask<Email> DeleteAsync(Guid id, bool saveChanges = true)
        {
            var foundEmail = await GetByIdAsync(id);

            if (foundEmail is null)
                throw new InvalidOperationException("You searched email not found");

            foundEmail.IsDeleted = true;
            await _appDataContext.SaveChangesAsync();

            return foundEmail;
        }

        public async ValueTask<Email> DeleteAsync(Email email, bool saveChanges = true)
        {
            var foundEmail = await GetByIdAsync(email.Id);

            if (foundEmail is null)
                throw new InvalidOperationException("You searched email not found");

            await _appDataContext.SaveChangesAsync();

            return foundEmail;
        }

        public IQueryable<Email> Get(Expression<Func<Email, bool>> expression)
        {
            return _appDataContext.Emails.Where(expression.Compile()).AsQueryable();
        }
        
        public ValueTask<ICollection<Email>> GetAsync(IEnumerable<Guid> ids)
        {
            var email = _appDataContext.Emails.Where(email => ids.Contains(email.Id));

            return new ValueTask<ICollection<Email>>(email.ToList());
        }

        public ValueTask<Email?> GetByIdAsync(Guid id)
        {
            var email = _appDataContext.Emails.FirstOrDefault(email => email.Id == id);

            return new ValueTask<Email?>(email);
        }

        public async ValueTask<Email> UpdateAsync(Email email, bool saveChanges = true)
        {
            var foundEmail = _appDataContext.Emails.FirstOrDefault(searched => searched.Id == email.Id);

            if (foundEmail is null)
                throw new InvalidOperationException("Email not found");

            foundEmail.SenderAddress = email.SenderAddress;
            foundEmail.ReceiverAddress = email.ReceiverAddress;
            foundEmail.Subject = email.Subject;
            foundEmail.Body = email.Body;
            foundEmail.SentTime = email.SentTime;
            foundEmail.IsSent = email.IsSent;

            await _appDataContext.SaveChangesAsync();
            return foundEmail;
        }
    }

}
