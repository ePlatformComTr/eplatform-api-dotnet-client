using System.Collections.Generic;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class BaseUblModel<TGeneral, TLine, TTax, TTotals, TPaymentMeans, TPaymentTerms, TOrderInfo, TArchiveInfo, TRelatedDespatch, TBuyerCustomerInfo, TTaxRepresentativePartyInfo, TUblSettings, TAllowanceCharge>
        where TGeneral : GeneralInfoBaseModel, new()
        where TLine : InvoiceLineBaseModel<TTax>
        where TTax : InvoiceLineTaxBaseModel
        where TTotals : InvoiceTotalsBaseModel, new()
        where TPaymentMeans : PaymentMeansBaseModel, new()
        where TPaymentTerms : PaymentTermsBaseModel, new()
        where TOrderInfo : OrderInfoBaseModel, new()
        where TArchiveInfo : ArchiveInfoBaseModel, new()
        where TRelatedDespatch : RelatedDespatchBaseModel
        where TBuyerCustomerInfo : BuyerCustomerInfoBaseModel, new()
        where TTaxRepresentativePartyInfo : TaxRepresentativePartyInfoBaseModel, new()
        where TUblSettings : UblSettingsModel, new()
        where TAllowanceCharge : AllowanceChargeModel, new()
    {
        public bool isSend { get; set; }
        public int RecordType { get; set; }
        public List<NoteModel> Notes { get; set; }
        public virtual TGeneral GeneralInfoModel { get; set; }
        public virtual List<TLine> InvoiceLines { get; set; }
        public virtual TTotals InvoiceTotalsModel { get; set; }
        public virtual TPaymentMeans PaymentMeansModel { get; set; }
        public virtual TPaymentTerms PaymentTermsModel { get; set; }
        public virtual TOrderInfo OrderInfoModel { get; set; }
        public virtual TArchiveInfo ArchiveInfoModel { get; set; }
        public virtual TUblSettings UblSettingsModel { get; set; }
        public virtual List<TRelatedDespatch> RelatedDespatchList { get; set; }
        public List<CustomDocumentReferenceModel> CustomDocumentReferenceList { get; set; }
        public AdditionalInvoiceTypeBaseModel AdditionalInvoiceTypeInfo { get; set; }
        public CurrentAccountAddressModel CustomerAddress { get; set; }
        public CurrentAccountAddressModel SupplierAddress { get; set; }
        public virtual TBuyerCustomerInfo BuyerCustomerInfoModel { get; set; }
        public virtual TTaxRepresentativePartyInfo TaxRepresentativePartyInfoModel { get; set; }
        public List<TAllowanceCharge> AllowanceCharges { get; set; }

        public BaseUblModel()
        {
            GeneralInfoModel = new TGeneral();
            InvoiceLines = new List<TLine>();
            InvoiceTotalsModel = new TTotals();
            PaymentMeansModel = new TPaymentMeans();
            PaymentTermsModel = new TPaymentTerms();
            OrderInfoModel = new TOrderInfo();
            ArchiveInfoModel = new TArchiveInfo();
            UblSettingsModel = new TUblSettings();
            RelatedDespatchList = new List<TRelatedDespatch>();
            BuyerCustomerInfoModel = new TBuyerCustomerInfo();
            TaxRepresentativePartyInfoModel = new TTaxRepresentativePartyInfo();
            AdditionalInvoiceTypeInfo = new AdditionalInvoiceTypeBaseModel();
            CustomDocumentReferenceList = new List<CustomDocumentReferenceModel>();
            AllowanceCharges = new List<TAllowanceCharge>();
        }
    }
}
