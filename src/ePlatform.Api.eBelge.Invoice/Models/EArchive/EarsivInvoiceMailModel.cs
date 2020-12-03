using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class EarsivInvoiceMailModel
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public string EmailAddress { get; set; }
        public int EmailStatus { get; set; }
        public int TryCount { get; set; }
        public DateTime? LastTryDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int Type { get; set; }
    }
}
