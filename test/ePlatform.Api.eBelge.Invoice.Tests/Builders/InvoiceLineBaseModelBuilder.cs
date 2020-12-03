using ePlatform.Api.eBelge.Invoice.Models;
using ePlatform.Api.eBelge.Invoice.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Invoice.Tests.Builders
{
    public class InvoiceLineBaseModelBuilder : BuilderBase<InvoiceLineBaseModel<InvoiceLineTaxBaseModel>, InvoiceLineBaseModelBuilder>
    {
        public override InvoiceLineBaseModelBuilder CreateWithDefaultValues()
        {
            _concreteObject = new InvoiceLineBaseModel<InvoiceLineTaxBaseModel>
            {
                Amount = 1,
                InventoryCard = "Test",
                DiscountRate = 0,
                DiscountAmount = 0,
                UnitCode = "C62",
                UnitPrice = 100,
                VatRate = 10,
                VatAmount = 10,
                LineExtensionAmount = 100
            };

            return this;
        }
    }
}
