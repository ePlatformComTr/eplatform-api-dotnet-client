using System;
using System.Collections.Generic;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class OutboxInvoiceModel : BaseInvoiceModel
    {
        public Guid? ResponseEnvelopeId { get; set; }
        public string LocalReferenceId { get; set; }
        public int TryCount { get; set; }
        public DateTime? LastTryDate { get; set; }
        public string Message { get; set; }
        public string ObjectStorageKey { get; set; }
        public InvoiceAppType AppType { get; set; }
        public string Reason { get; set; }
        public string Prefix { get; set; }
        public string XsltCode { get; set; }
        public string TaxOffice { get; set; }
        public EarsivInvoiceModel EarsivInvoice { get; set; }
        public List<InvoiceTaxModel> EfaturaOutboxInvoiceTax { get; set; }
    }
}
