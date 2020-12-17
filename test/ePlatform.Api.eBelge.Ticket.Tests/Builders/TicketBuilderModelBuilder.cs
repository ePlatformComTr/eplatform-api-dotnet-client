using System;
using System.Collections.Generic;
using ePlatform.Api.eBelge.Ticket.Common.Enums;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Ticket.Tests.Builders
{
    public class TicketBuilderModelBuilder : BuilderBase<TicketBuilderModel, TicketBuilderModelBuilder>
    {
        private readonly IBuilder<TicketLine, TicketLineBuilder> _ticketLineBuilder;
        private readonly IBuilder<NoteModel, NoteModelBuilder> _noteModelBuilder;
        public TicketBuilderModelBuilder(IBuilder<TicketLine, TicketLineBuilder> ticketLineBuilder,
            IBuilder<NoteModel, NoteModelBuilder> noteModelBuilder)
        {
            _ticketLineBuilder = ticketLineBuilder;
            _noteModelBuilder = noteModelBuilder;
        }
        public override TicketBuilderModelBuilder CreateWithDefaultValues()
        {
            _concreteObject = new TicketBuilderModel
            {
                Ettn = Guid.NewGuid(),
                Prefix = "ABC",
                ReferenceNumber = "RFR1344",
                Status = TicketStatus.Draft,
                TicketDate = DateTime.Now,
                TicketNumber = $"ABC{DateTime.Now.Year}{new Random().Next(100000000, 999999999)}",
                CurrencyCode = Currency.TRY,
                CustomerEmail = "johndoe@eplatform.com.tr",
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                DocumentType = DocumentType.SATIS,
                ExchangeRate = 0,
                Identifier = "27337872323",
                CustomerStreet = "test customer street",
                CustomerBuildingName = "test customer building name",
                CustomerBuildingNo = "test building no",
                CustomerDoorNo = "test customer door no",
                CustomerTown = "test customer town",
                CustomerDistrict = "test customer district",
                CustomerCity = "test customer city",
                CustomerTelephone = "5367766470",
                IsEmailSend = true,
                PaymentType = PaymentType.DIGER,
                PaymentDescription = "test payment description",
                SeatNumber = "A34",
                TicketLines = new List<TicketLine>
                {
                    _ticketLineBuilder.CreateWithDefaultValues().Build(),
                },
                Notes = new List<NoteModel>
                {
                    _noteModelBuilder.CreateWithDefaultValues().Build(),
                    _noteModelBuilder.Create().With(x => x.Note = "test note - 2").Build()
                }
            };
            return this;
        }
        public TicketBuilderModelBuilder CreateWithDefaultEventTicketValues()
        {
            var eventTicket = CreateWithDefaultValues()
                .With(et => et.TicketType = TicketType.ETKINLIK)
                .With(et => et.EventTime = DateTime.Now.AddDays(2))
                .With(et => et.EventName = "test event name")
                .With(et => et.EventLocation = "test event location")
                .With(et => et.EventCity = "test event city")
                .With(et => et.EventCityId = 80)
                .With(et => et.EventMunicipality = "test event municipality")
                .With(et => et.EventDescription = "test event description")
                .With(et => et.EventOrganizerVknTckn = "12345678901")
                .Build();

            _concreteObject = eventTicket;
            return this;
        }
        public TicketBuilderModelBuilder CreateWithDefaultPassengerTicketValues()
        {
            var passengerTicket = CreateWithDefaultValues()
                .With(et => et.TicketType = TicketType.YOLCU)
                .With(et => et.VehiclePlate = "34ABC34")
                .With(et => et.DepartureDate = DateTime.Now.AddDays(2))
                .With(et => et.ExpeditionTime = DateTime.Now.AddDays(2))
                .With(et => et.ExpeditionNumber = "123")
                .With(et => et.DepartureLocation = "test departure location")
                .Build();

            _concreteObject = passengerTicket;
            return this;
        }
    }
}
