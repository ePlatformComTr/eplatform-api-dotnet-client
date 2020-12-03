using System;

namespace ePlatform.Api.eBelge.Ticket.EventTicket.Models
{
    public class EventTicketModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string TicketNumber { get; set; }
        public string TargetTitle { get; set; }
        public string TargetVknTckn { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string LocalReferenceId { get; set; }
        public bool IsArchived { get; set; }
        public string Currency { get; set; }
        public decimal? CurrencyRate { get; set; }
        public byte DocumentType { get; set; }
        public string PaymentType { get; set; }
        public string PaymentDescription { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? TotalVat { get; set; }
        public int Status { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string LocationCityCode { get; set; }
        public string LocationMunicipality { get; set; }
        public string LocationDescription { get; set; }
        public string EventOrganizerVknTckn { get; set; }
        public string RecordExpensesVknTckn { get; set; }
        public string RecordExpensesTitle { get; set; }
        public string SeatNumber { get; set; }
        public string Message { get; set; }
        public int EmailStatus { get; set; }
        public int TryCount { get; set; }
        public DateTime? LastTryDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string XsltCode { get; set; }
        public decimal? PayableAmount { get; set; }
    }
}
