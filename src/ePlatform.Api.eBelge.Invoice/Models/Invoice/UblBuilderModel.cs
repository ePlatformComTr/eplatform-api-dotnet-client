using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class UblBuilderModel : BaseUblModel<GeneralInfoBaseModel, InvoiceLineBaseModel<InvoiceLineTaxBaseModel>, InvoiceLineTaxBaseModel,
        InvoiceTotalsBaseModel, PaymentMeansBaseModel, PaymentTermsBaseModel, OrderInfoBaseModel, ArchiveInfoBaseModel,
        RelatedDespatchBaseModel, BuyerCustomerInfoBaseModel, TaxRepresentativePartyInfoBaseModel, UblSettingsModel, AllowanceChargeModel>
    {
        public Guid InvoiceId { get; set; }
        public int Status { get; set; }
        public bool IsNew { get; set; }
        public string XsltCode { get; set; }
        public string LocalReferenceId { get; set; }
        public bool UseManualInvoiceId { get; set; }
        public AddressBookModel AddressBook { get; set; }
        public EArsivInfoModel EArsivInfo { get; set; }
    }
}
