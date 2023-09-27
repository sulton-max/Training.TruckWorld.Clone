﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities
{
    public class EmailTemplate: SoftDeletedEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailTemplate() { }
        public EmailTemplate(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }
    }
}
