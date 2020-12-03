using System;

namespace ePlatform.Api.eBelge.Ticket.EventTicket.Models
{
    public class EventTicketStatusModel
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public int EmailStatus { get; set; }
    }
}
