using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class GeneralInfoBaseModel
    {
        public Guid Ettn { get; set; }
        public string Prefix { get; set; }
        public string CustomizationId { get; set; }
        public string InvoiceNumber { get; set; }
        public string SlipNumber { get; set; }
        public InvoiceProfileType? InvoiceProfileType { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? IssueTime { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public InvoiceType Type { get; set; }
        public string ReturnInvoiceNumber { get; set; }
        public DateTime? ReturnInvoiceDate { get; set; }
        public long CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public string DespatchNumber { get; set; }
        public DespatchAdviceType DespatchType { get; set; }
        public DespatchProfileType? DespatchProfileType { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
