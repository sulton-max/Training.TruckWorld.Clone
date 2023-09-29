using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Notifications.Models;

namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        public ValueTask<EmailMessage> ConvertToMessage(EmailTemplate template, Dictionary<string, string> values, string sender, string receiver)
        {
            var body = new StringBuilder(template.Body);


            foreach (var item in values)
            {
                body = body.Replace(item.Key, item.Value);
            }

            var emailMessage = new EmailMessage(sender, receiver, template.Subject, body.ToString());
            return ValueTask.FromResult(emailMessage);
        }
    }
}
