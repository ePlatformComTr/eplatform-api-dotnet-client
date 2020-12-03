using System.Collections.Generic;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Ticket.Tests.Builders
{
    public class TaxModelBuilder : BuilderBase<TaxModel, TaxModelBuilder>
    {
        public override TaxModelBuilder CreateWithDefaultValues()
        {
            _concreteObject = new TaxModel
            {
                TaxCode = "0030",
                TaxName = "test tax name - 1",
                TaxRate = 20,
                TaxAmount = 100
            };
            return this;
        }
    }
}
