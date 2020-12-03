using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class PaymentMeansBaseModel
    {
        public PaymentMeansType PaymentMeansCode { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public string PaymentChannelCode { get; set; }
        public string InstructionNote { get; set; }
        public string PayeeFinancialAccountId { get; set; }
        public string PayeeFinancialAccountCurrencyCode { get; set; }
    }
}
