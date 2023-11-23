using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators
{
    public class EmailTemplateValidator : AbstractValidator<EmailTemplate>
    {
        public EmailTemplateValidator()
        {
            RuleFor(template => template.Content)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(256);

            RuleFor(template => template.Type)
                .Equal(NotificationType.Email);
        }
    }
}
