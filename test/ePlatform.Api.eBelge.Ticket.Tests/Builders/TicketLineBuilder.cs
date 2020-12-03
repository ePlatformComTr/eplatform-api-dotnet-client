using System.Collections.Generic;
using ePlatform.Api.eBelge.Ticket.Common.Enums;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Ticket.Tests.Builders
{
    public class TicketLineBuilder : BuilderBase<TicketLine, TicketLineBuilder>
    {
        private readonly IBuilder<TaxModel, TaxModelBuilder> _taxModelBuilder;
        public TicketLineBuilder(IBuilder<TaxModel, TaxModelBuilder> taxModelBuilder)
        {
            _taxModelBuilder = taxModelBuilder;
        }
        public override TicketLineBuilder CreateWithDefaultValues()
        {
            _concreteObject = new TicketLine
            {
                ServiceType = ServiceType.DIGER,
                ServiceDescription = "test service description",
                Amount = 100,
                DiscountRate = 10,
                DiscountAmount = 10,
                VatRate = 18,
                VatAmount = 16.2m,
                Taxes = new List<TaxModel> {_taxModelBuilder.CreateWithDefaultValues().Build()}
            };
            return this;
        }
    }
}
