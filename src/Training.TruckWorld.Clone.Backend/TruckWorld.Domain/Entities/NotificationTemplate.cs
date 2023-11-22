using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckWorld.Domain.Common.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Domain.Entities
{
    public abstract class NotificationTemplate : IEntity
    {
        public Guid Id { get; set; }

        public NotificationType Type { get; set; }

        public NotificationTemplateType TemplateType { get; set; }

        public string Content { get; set; } = default!;
    }
}
