using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;
using Training.TruckWorld.Backend.Domain.Exceptions;
using System;

namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Services
{
    public class EmailService : IEmailService
    {
        private readonly IDataContext _appDataContext;

        public EmailService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<Email> CreateAsync(Email email, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            await _appDataContext.Emails.AddAsync(email, cancellationToken);

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();

            return email;
        }

        public async ValueTask<Email> DeleteAsync(Guid id, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            var foundEmail = await GetByIdAsync(id) ?? throw new EntityNotFoundException(typeof(Email));

            if (foundEmail.IsDeleted)
                throw new EntityDeletedException(typeof(Email), foundEmail.Id);


            await _appDataContext.Emails.RemoveAsync(foundEmail, cancellationToken);

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();

            return foundEmail;
        }

        public ValueTask<Email> DeleteAsync(Email email, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            return DeleteAsync(email.Id, saveChanges, cancellationToken);
        }

        public IQueryable<Email> Get(Expression<Func<Email, bool>> predicate)
        => _appDataContext.Emails.Where(predicate.Compile()).AsQueryable();

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

        public async ValueTask<Email> UpdateAsync(Email email, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            var foundEmail = _appDataContext.Emails.FirstOrDefault(searched => searched.Id == email.Id)
                             ?? throw new EntityNotFoundException(typeof(Email));


            foundEmail.SenderAddress = email.SenderAddress;
            foundEmail.ReceiverAddress = email.ReceiverAddress;
            foundEmail.Subject = email.Subject;
            foundEmail.Body = email.Body;
            foundEmail.SentTime = email.SentTime;
            foundEmail.IsSent = email.IsSent;

            await _appDataContext.Emails.UpdateAsync(foundEmail, cancellationToken);

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            
            return foundEmail;
        }
    }
}