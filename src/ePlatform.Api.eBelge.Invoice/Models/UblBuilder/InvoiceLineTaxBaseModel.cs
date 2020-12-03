namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class InvoiceLineTaxBaseModel
    {
        public string TaxTypeCode { get; set; }
        public string TaxName { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public string VatExemptionReasonCode { get; set; }
        public string VatExemptionReason { get; set; }
        public bool IsWithHolding => TaxTypeCode == "9015";
        public bool IsNegative => TaxTypeCode == "0003" || TaxTypeCode == "0011" || (TaxTypeCode == "9015" || TaxTypeCode == "4171");
        public long? WithHoldingId { get; set; }
        public string WithHoldingCode { get; set; }
        public decimal TaxableAmount { get; set; }

        internal InvoiceLineTaxBaseModel Clone() => new InvoiceLineTaxBaseModel()
        {
            TaxTypeCode = TaxTypeCode,
            TaxName = TaxName,
            TaxRate = TaxRate,
            TaxAmount = TaxAmount,
            TaxableAmount = TaxableAmount,
            VatExemptionReasonCode = VatExemptionReasonCode,
            VatExemptionReason = VatExemptionReason,
            WithHoldingId = WithHoldingId,
            WithHoldingCode = WithHoldingCode
        };
    }
}
