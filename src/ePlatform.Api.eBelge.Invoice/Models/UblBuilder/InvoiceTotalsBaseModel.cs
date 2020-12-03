using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class InvoiceTotalsBaseModel
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
        public bool IsNew { get; set; }
        public string XsltCode { get; set; }
        public string LocalReferenceId { get; set; }
        public bool UseManualProducerReceiptId { get; set; }
        public AddressBookModel AddressBook { get; set; }
        public int SendType { get; set; }
        public bool SendEMail { get; set; }
        public string EMailAddress { get; set; }
    }
}
