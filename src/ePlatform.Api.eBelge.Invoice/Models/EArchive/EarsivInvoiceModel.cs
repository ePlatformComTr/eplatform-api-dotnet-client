using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class EarsivInvoiceModel
    {
        public Guid Id { get; set; }
        public EArsivFileType FileType { get; set; }
        public EArsivSendType SendType { get; set; }
        public string FileName { get; set; }
        public string SummaryHash { get; set; }
        public TimeSpan InvoiceTime { get; set; }
        public string TargetLocalId { get; set; }
        public bool IsInternetSale { get; set; }
        public string InternetWebAddress { get; set; }
        public InternetPaymentType? InternetPaymentType { get; set; }
        public string InternetPaymentTypeOther { get; set; }
        public string InternetPaymentAgent { get; set; }
        public DateTime? InternetPaymentDate { get; set; }
        public DateTime? InternetCargoDate { get; set; }
        public string InternetCargoIdentifier { get; set; }
        public string InternetCargoFullName { get; set; }
        public string OkcIdentifier { get; set; }
        public string OkcZNumber { get; set; }
        public string OkcVoucherNumber { get; set; }
        public VoucherType OkcVoucherType { get; set; }
        public DateTime? OkcVoucherCreatedDate { get; set; }
        public TimeSpan? OkcVoucherCreatedTime { get; set; }
        public bool SendEMail { get; set; }
        public string EMailAddress { get; set; }
        public List<EarsivInvoiceMailModel> EMailAddressList { get; set; }
        public EarsivEmailStatus EMailStatus { get; set; }



        [NotMapped]
        public string CustomerName { get; set; }

        [NotMapped]
        public OutboxInvoiceModel OutboxInvoice { get; set; }

        [NotMapped]
        public BaseInvoiceModel BaseInvoice { get; set; }

        [NotMapped]
        public List<OutboxInvoiceTaxModel> TaxList { get; set; }
        [NotMapped]
        public List<OutboxInvoiceTaxModel> WithholdingList { get; set; }
    }
}
