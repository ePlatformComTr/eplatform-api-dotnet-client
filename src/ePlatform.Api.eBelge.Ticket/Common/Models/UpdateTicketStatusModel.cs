using System;
using System.Collections.Generic;
using ePlatform.Api.eBelge.Ticket.Common.Enums;

namespace ePlatform.Api.eBelge.Ticket.Common.Models
{
    public class UpdateTicketStatusModel
    {
        public List<Guid> Ids { get; set; }
        public TicketStatus Status { get; set; }
    }
}
