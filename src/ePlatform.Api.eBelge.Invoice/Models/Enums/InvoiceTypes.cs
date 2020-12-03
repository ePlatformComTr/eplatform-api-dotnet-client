namespace ePlatform.Api.eBelge.Invoice.Models
{
    public enum InvoiceTypes
    {
        /// <summary>
        ///Temel
        /// </summary>
        BaseInvoice = 0,
        /// <summary>
        ///Ticari
        /// </summary>
        CommercialInvoice = 1,
        /// <summary>
        ///İhracat
        /// </summary>
        ExportInvoice = 2,
        /// <summary>
        ///Yolcu Beraberinde
        /// </summary>
        PassengerInvoice = 3,
        /// <summary>
        ///e-Arşiv
        /// </summary>
        EArchiveInvoice = 4,
        /// <summary>
        /// KAGIT
        /// </summary>
        KAGIT = 5,
        /// <summary>
        /// HKS
        /// </summary>
        HKS = 6,
        /// <summary>
        /// EARSIVBELGE
        /// </summary>
        EARSIVBELGE = 7
    }
}
