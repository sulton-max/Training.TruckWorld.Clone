﻿using System.Net;
using System.Net.Mail;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Infrastructure.Notifications.Models;

namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    public async ValueTask<bool> SendEmailAsync(EmailMessage emailMessage)
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
