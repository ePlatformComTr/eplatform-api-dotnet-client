using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class InvoiceTaxModel
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public int InvoiceTaxType { get; set; }
        public decimal Assessment { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public int Rate { get; set; }
    }
}