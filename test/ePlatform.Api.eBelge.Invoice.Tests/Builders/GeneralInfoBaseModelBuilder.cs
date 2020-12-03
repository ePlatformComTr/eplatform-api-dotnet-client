using System;
using ePlatform.Api.eBelge.Invoice.Models;
using ePlatform.Api.eBelge.Invoice.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Invoice.Tests.Builders
{
    public class GeneralInfoBaseModelBuilder : BuilderBase<GeneralInfoBaseModel, GeneralInfoBaseModelBuilder>
    {
        public override GeneralInfoBaseModelBuilder CreateWithDefaultValues()
        {
            _concreteObject = new GeneralInfoBaseModel
            {
                Ettn = Guid.NewGuid(),
                Prefix = null,
                InvoiceNumber = null,
                InvoiceProfileType = InvoiceProfileType.TEMELFATURA,
                IssueDate = DateTime.Now,
                Type = InvoiceType.SATIS,
                CurrencyCode = "TRY"
            };

            return this;
        }
    }
}
