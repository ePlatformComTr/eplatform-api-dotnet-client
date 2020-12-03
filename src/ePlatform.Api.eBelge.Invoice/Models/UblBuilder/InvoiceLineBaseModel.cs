using System.Collections.Generic;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class InvoiceLineBaseModel<TTax> where TTax : InvoiceLineTaxBaseModel
    {
        public long InventoryServiceType { get; set; }
        public string InventoryCard { get; set; }
        public bool SerialNoEnabled { get; set; }
        public decimal Amount { get; set; }
        public long UnitCodeId { get; set; }
        public string UnitCode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal LineExtensionAmount { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public string VatExemptionReasonCode { get; set; }
        public string VatExemptionReason { get; set; }
        public virtual List<TTax> Taxes { get; set; }
        public InvoiceLineDeliveryInfoBaseModel InvoiceLineDeliveryInfoModel { get; set; }
        public List<string> SerialNumberList { get; set; }
        public string TagNumber { get; set; }
        public string GoodsOwnerName { get; set; }
        public string GoodsOwnerIdentifier { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string SellersItemIdentification { get; set; }
        public string BuyersItemIdentification { get; set; }
        public string ManufacturersItemIdentification { get; set; }

        public InvoiceLineBaseModel()
        {
            Taxes = new List<TTax>();
            SerialNumberList = new List<string>();
            InvoiceLineDeliveryInfoModel = new InvoiceLineDeliveryInfoBaseModel();
        }
    }
}
