namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class EArsivInvoiceGetModel
    {
        public bool IsInternetSale { get; set; }
        public EarsivEmailStatus EMailStatus { get; set; }
        public bool SendEmail { get; set; }
        public int SendType { get; set; }
        public string EmailAddress { get; set; }
    }
}
