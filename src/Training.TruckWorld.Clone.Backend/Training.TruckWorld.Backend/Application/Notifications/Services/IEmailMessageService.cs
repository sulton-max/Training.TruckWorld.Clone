﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Notifications.Models;

namespace Training.TruckWorld.Backend.Application.Notifications.Services
{
    public interface IEmailMessageService
    {
        ValueTask<EmailMessage> ConvertToMessage(EmailTemplate template, Dictionary<string, string> values, string sender, string receiver);
    }
}
