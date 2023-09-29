using System.Net;
using System.Net.Mail;

using Training.TruckWorld.Backend.Application.Emails.Interfaces;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Infrastructure.Emails.Services;

public class EmailSenderService : IEmailSenderService
{
    public async ValueTask<bool> SendEmailAsync(Email emailMessage)
    {
        var sendEmailMessage = false;

        try
        {
            using (var smtp = new SmtpClient("smpt.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("username", "password");
                
                smtp.EnableSsl = true;

                var mail = new MailMessage(emailMessage.SenderAddress, emailMessage.ReceiverAddress);
                
                mail.Subject = emailMessage.Subject;
            
                mail.Body = emailMessage.Body;
              
                await smtp.SendMailAsync(mail);
            }
            emailMessage.IsSent = true;
           
            emailMessage.SentTime = DateTimeOffset.UtcNow;
           
            sendEmailMessage = true;
        }
        catch (Exception ex)
        {
            emailMessage.IsSent = false;
          
            emailMessage.SentTime = DateTimeOffset.UtcNow;
         
            sendEmailMessage = false;
        }
        
        return sendEmailMessage;
    }
}
