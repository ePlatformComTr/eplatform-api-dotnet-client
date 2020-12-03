using System.Collections.Generic;
using ePlatform.Api.eBelge.Invoice.Models;
using ePlatform.Api.eBelge.Invoice.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Invoice.Tests.Builders
{
    public class UblBuilderModelBuilder : BuilderBase<UblBuilderModel, UblBuilderModelBuilder>
    {
        private readonly IBuilder<GeneralInfoBaseModel, GeneralInfoBaseModelBuilder> _generalInfoBaseModelBuilder;
        private readonly IBuilder<AddressBookModel, AddressBookModelBuilder> _addressBookModelBuilder;
        private readonly IBuilder<InvoiceLineBaseModel<InvoiceLineTaxBaseModel>, InvoiceLineBaseModelBuilder> _invoiceLineBaseModelBuilder;

        public UblBuilderModelBuilder(IBuilder<GeneralInfoBaseModel, GeneralInfoBaseModelBuilder> generalInfoBaseModelBuilder,
            IBuilder<AddressBookModel, AddressBookModelBuilder> addressBookModelBuilder,
            IBuilder<InvoiceLineBaseModel<InvoiceLineTaxBaseModel>, InvoiceLineBaseModelBuilder> invoiceLineBaseModelBuilder)
        {
            _generalInfoBaseModelBuilder = generalInfoBaseModelBuilder;
            _addressBookModelBuilder = addressBookModelBuilder;
            _invoiceLineBaseModelBuilder = invoiceLineBaseModelBuilder;
        }

        public override UblBuilderModelBuilder CreateWithDefaultValues()
        {
            _concreteObject = new UblBuilderModel
            {
                Status = (int)InvoiceStatus.Draft,
                XsltCode = null,
                UseManualInvoiceId = false,
                RecordType = (int)RecordType.Invoice,
                GeneralInfoModel = _generalInfoBaseModelBuilder.CreateWithDefaultValues().Build(),
                AddressBook = _addressBookModelBuilder.CreateWithDefaultValues().Build(),
                InvoiceLines = new List<InvoiceLineBaseModel<InvoiceLineTaxBaseModel>> {_invoiceLineBaseModelBuilder.CreateWithDefaultValues().Build()}
            };

            return this;
        }
    }
}
