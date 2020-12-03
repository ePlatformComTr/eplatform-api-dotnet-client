using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class InboxInvoiceGetModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? EnvelopeId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int TipType { get; set; }
        public string TargetTitle { get; set; }
        public string TargetVknTckn { get; set; }
        public string TargetAlias { get; set; }
        public bool IsArchived { get; set; }
        public bool IsAgentNew { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public string Currency { get; set; }
        public DateTime? SentDate { get; set; }
        public bool IsNew { get; set; }
        public bool IsRead { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DigestValue { get; set; }
        public BaseEnvelopeGetModel Envelope { get; set; }
    }
}
