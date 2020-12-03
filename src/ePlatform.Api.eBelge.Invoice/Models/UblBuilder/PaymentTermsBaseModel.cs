namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class PaymentTermsBaseModel
    {
        public decimal? Amount { get; set; }
        public string Note { get; set; }
        public decimal? PenaltySurchargePercent { get; set; }
    }
}
