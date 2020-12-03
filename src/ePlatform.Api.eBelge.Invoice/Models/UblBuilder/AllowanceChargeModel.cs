namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class AllowanceChargeModel
    {
        public bool UseCalculatedVatAmount { get; set; }
        public bool UseCalculatedTotalSummary { get; set; }
        public bool? HideDespatchMessage { get; set; }
        public bool? IsFit { get; set; }
    }
}
