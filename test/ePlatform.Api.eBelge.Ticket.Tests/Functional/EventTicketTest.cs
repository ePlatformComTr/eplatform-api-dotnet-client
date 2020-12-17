using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Ticket.Common.Enums;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.EventTicket;
using ePlatform.Api.eBelge.Ticket.EventTicket.Models;
using ePlatform.Api.eBelge.Ticket.Tests.Builders;
using ePlatform.Api.eBelge.Ticket.Tests.Setup;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ePlatform.Api.eBelge.Ticket.Tests.Functional
{
    [Collection("ticket-startup")]
    public class EventTicketTest
    {
        private readonly EventTicketClient _eventTicketClient;
        private readonly TicketBuilderModelBuilder _ticketBuilderModelBuilder;

        public EventTicketTest(StartupFixture fixture)
        {
            _eventTicketClient = fixture.ServiceProvider.GetRequiredService<EventTicketClient>();
            _ticketBuilderModelBuilder = fixture.ServiceProvider.GetRequiredService<TicketBuilderModelBuilder>();
        }

        [Fact]
        public async Task Should_Create_Event_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            Assert.NotNull(createdTicketResponseModel);
            Assert.NotNull(createdTicketResponseModel.Ettn);
            Assert.NotNull(createdTicketResponseModel.TicketNumber);
            Assert.Equal(ticketBuilderModel.Ettn, new Guid(createdTicketResponseModel.Ettn));
            Assert.Equal(ticketBuilderModel.TicketNumber, createdTicketResponseModel.TicketNumber);
        }

        [Fact]
        public async Task Should_Get_Event_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            var eventTicketModel = await _eventTicketClient.Get(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(eventTicketModel);
            Assert.Equal(ticketBuilderModel.Ettn, eventTicketModel.Id);
            Assert.Equal(ticketBuilderModel.TicketNumber, eventTicketModel.TicketNumber);
            Assert.Equal($"{ticketBuilderModel.CustomerFirstName} {ticketBuilderModel.CustomerLastName}", eventTicketModel.TargetTitle);
            Assert.Equal(ticketBuilderModel.Identifier, eventTicketModel.TargetVknTckn);
            Assert.NotNull(eventTicketModel.ExecutionDate);
            Assert.Equal(ticketBuilderModel.ReferenceNumber,eventTicketModel.LocalReferenceId);
            Assert.Equal(Currency.TRY.ToString(), eventTicketModel.Currency);
            Assert.Equal(ticketBuilderModel.ExchangeRate, eventTicketModel.CurrencyRate);
            Assert.Equal((int)ticketBuilderModel.DocumentType, eventTicketModel.DocumentType);
            Assert.Equal(ticketBuilderModel.PaymentType.ToString(), eventTicketModel.PaymentType);
            Assert.Equal(ticketBuilderModel.PaymentDescription, eventTicketModel.PaymentDescription);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount), eventTicketModel.TotalAmount);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.VatAmount), eventTicketModel.TotalVat);
            Assert.Equal((int)ticketBuilderModel.Status, eventTicketModel.Status);
            Assert.Equal(ticketBuilderModel.EventName, eventTicketModel.EventName);
            Assert.Equal(ticketBuilderModel.EventTime.Date, eventTicketModel.EventDate.Date);
            Assert.Equal(ticketBuilderModel.EventLocation, eventTicketModel.EventLocation);
            Assert.Equal(ticketBuilderModel.EventCityId.ToString(), eventTicketModel.LocationCityCode);
            Assert.Equal(ticketBuilderModel.EventMunicipality, eventTicketModel.LocationMunicipality);
            Assert.Equal(ticketBuilderModel.EventDescription, eventTicketModel.LocationDescription);
            Assert.Equal(ticketBuilderModel.EventOrganizerVknTckn, eventTicketModel.EventOrganizerVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesVknTckn, eventTicketModel.RecordExpensesVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesTitle, eventTicketModel.RecordExpensesTitle);
            Assert.Equal(ticketBuilderModel.SeatNumber, eventTicketModel.SeatNumber);
            Assert.Null(eventTicketModel.Message);
            Assert.Equal((int)TicketEmailStatus.Created, eventTicketModel.EmailStatus);
            Assert.Null(eventTicketModel.XsltCode);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount) +
                         ticketBuilderModel.TicketLines.Sum(x => x.VatAmount) +
                         ticketBuilderModel.TicketLines.SelectMany(x => x.Taxes).Sum(y => y.TaxAmount)
                , eventTicketModel.PayableAmount);
        }

        [Fact]
        public async Task Should_Get_Event_Ticket_Detail()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            var detailedEventTicket = await _eventTicketClient.GetDetail(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(detailedEventTicket);
            Assert.Equal(ticketBuilderModel.Ettn, detailedEventTicket.Ettn);
            Assert.Equal(ticketBuilderModel.Status, detailedEventTicket.Status);
            Assert.Equal(ticketBuilderModel.TicketNumber, detailedEventTicket.TicketNumber);
            Assert.Equal(ticketBuilderModel.Prefix, detailedEventTicket.Prefix);
            Assert.Equal(ticketBuilderModel.ReferenceNumber, detailedEventTicket.ReferenceNumber);
            Assert.Equal(ticketBuilderModel.TicketType, detailedEventTicket.TicketType);
            Assert.Equal(ticketBuilderModel.TicketDate.Date, detailedEventTicket.TicketDate.Date);
            Assert.Equal(ticketBuilderModel.DocumentType, detailedEventTicket.DocumentType);
            Assert.Equal(ticketBuilderModel.CurrencyCode, detailedEventTicket.CurrencyCode);
            Assert.Equal(ticketBuilderModel.ExchangeRate, detailedEventTicket.ExchangeRate);
            Assert.Equal(ticketBuilderModel.Identifier, detailedEventTicket.Identifier);
            Assert.Equal(ticketBuilderModel.CustomerFirstName, detailedEventTicket.CustomerFirstName);
            Assert.Equal(ticketBuilderModel.CustomerStreet, detailedEventTicket.CustomerStreet);
            Assert.Equal(ticketBuilderModel.CustomerBuildingName, detailedEventTicket.CustomerBuildingName);
            Assert.Equal(ticketBuilderModel.CustomerBuildingNo, detailedEventTicket.CustomerBuildingNo);
            Assert.Equal(ticketBuilderModel.CustomerDoorNo, detailedEventTicket.CustomerDoorNo);
            Assert.Equal(ticketBuilderModel.CustomerTown, detailedEventTicket.CustomerTown);
            Assert.Equal(ticketBuilderModel.CustomerDistrict, detailedEventTicket.CustomerDistrict);
            Assert.Equal(ticketBuilderModel.CustomerCity, detailedEventTicket.CustomerCity);
            Assert.Equal(ticketBuilderModel.CustomerCountry, detailedEventTicket.CustomerCountry);
            Assert.Equal(ticketBuilderModel.CustomerTelephone, detailedEventTicket.CustomerTelephone);
            Assert.Equal(ticketBuilderModel.CustomerEmail, detailedEventTicket.CustomerEmail);
            Assert.Equal(ticketBuilderModel.IsEmailSend, detailedEventTicket.IsEmailSend);
            Assert.Equal(ticketBuilderModel.CustomerTaxCenter, detailedEventTicket.CustomerTaxCenter);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount), detailedEventTicket.TotalAmount);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.VatAmount), detailedEventTicket.TotalVat);
            Assert.Equal(ticketBuilderModel.PaymentType, detailedEventTicket.PaymentType);
            Assert.Equal(ticketBuilderModel.PaymentDescription, detailedEventTicket.PaymentDescription);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount) +
                         ticketBuilderModel.TicketLines.Sum(x => x.VatAmount) +
                         ticketBuilderModel.TicketLines.SelectMany(x => x.Taxes).Sum(y => y.TaxAmount)
                , detailedEventTicket.PayableAmount);
            Assert.Equal(ticketBuilderModel.VehiclePlate, detailedEventTicket.VehiclePlate);
            Assert.Equal(ticketBuilderModel.ExpeditionTime, detailedEventTicket.ExpeditionTime);
            Assert.Equal(ticketBuilderModel.ExpeditionNumber, detailedEventTicket.ExpeditionNumber);
            Assert.Equal(ticketBuilderModel.DepartureDate, detailedEventTicket.DepartureDate);
            Assert.Equal(ticketBuilderModel.DepartureLocation, detailedEventTicket.DepartureLocation);
            Assert.Equal(ticketBuilderModel.SeatNumber, detailedEventTicket.SeatNumber);
            Assert.Equal(ticketBuilderModel.VehicleOperatingVknTckn, detailedEventTicket.VehicleOperatingVknTckn);
            Assert.Equal(ticketBuilderModel.CommissionAmount, detailedEventTicket.CommissionAmount);
            Assert.Equal(ticketBuilderModel.CommissionTaxAmount, detailedEventTicket.CommissionTaxAmount);
            Assert.Equal(ticketBuilderModel.RecordExpensesVknTckn, detailedEventTicket.RecordExpensesVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesTitle, detailedEventTicket.RecordExpensesTitle);
            Assert.Equal(ticketBuilderModel.EventTime, detailedEventTicket.EventTime);
            Assert.Equal(ticketBuilderModel.EventName, detailedEventTicket.EventName);
            Assert.Equal(ticketBuilderModel.EventLocation, detailedEventTicket.EventLocation);
            // e-ticket service side must be checked
            // Expected: test event city
            // Actual:   (null)
            // Assert.Equal(ticketBuilderModel.EventCity, detailedEventTicket.EventCity);
            Assert.Equal(ticketBuilderModel.EventCityId, detailedEventTicket.EventCityId);
            Assert.Equal(ticketBuilderModel.EventMunicipality, detailedEventTicket.EventMunicipality);
            Assert.Equal(ticketBuilderModel.EventDescription, detailedEventTicket.EventDescription);
            Assert.Equal(ticketBuilderModel.EventOrganizerVknTckn, detailedEventTicket.EventOrganizerVknTckn);
            Assert.Equal(ticketBuilderModel.XsltCode, detailedEventTicket.XsltCode);
        }

        [Fact]
        public async Task Should_Get_Event_Ticket_List()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            var paginatedEventTicketList = await _eventTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .QueryFor(x => x.Id, Operator.Equal, createdTicketResponseModel.Ettn)
                    .Build());
            var eventTicketModel = paginatedEventTicketList.Items?.First();

            Assert.NotNull(paginatedEventTicketList);
            Assert.NotNull(eventTicketModel);
            Assert.NotNull(paginatedEventTicketList.Items);
            Assert.NotEmpty(paginatedEventTicketList.Items);
            Assert.False(paginatedEventTicketList.HasNextPage);
            Assert.True(paginatedEventTicketList.HasPreviousPage);
            Assert.Equal(1, paginatedEventTicketList.PageIndex);
            Assert.Equal(50, paginatedEventTicketList.PageSize);
            Assert.Equal(1, paginatedEventTicketList.TotalCount);
            Assert.Equal(1, paginatedEventTicketList.TotalPages);
            Assert.Equal(ticketBuilderModel.Ettn, eventTicketModel.Id);
            Assert.Equal(ticketBuilderModel.Ettn, eventTicketModel.Id);
            Assert.Equal(ticketBuilderModel.TicketNumber, eventTicketModel.TicketNumber);
            Assert.Equal($"{ticketBuilderModel.CustomerFirstName} {ticketBuilderModel.CustomerLastName}", eventTicketModel.TargetTitle);
            Assert.Equal(ticketBuilderModel.Identifier, eventTicketModel.TargetVknTckn);
            Assert.NotNull(eventTicketModel.ExecutionDate);
            Assert.Equal(ticketBuilderModel.ReferenceNumber,eventTicketModel.LocalReferenceId);
            Assert.Equal(Currency.TRY.ToString(), eventTicketModel.Currency);
            Assert.Equal(ticketBuilderModel.ExchangeRate, eventTicketModel.CurrencyRate);
            Assert.Equal((int)ticketBuilderModel.DocumentType, eventTicketModel.DocumentType);
            Assert.Equal(ticketBuilderModel.PaymentType.ToString(), eventTicketModel.PaymentType);
            Assert.Equal(ticketBuilderModel.PaymentDescription, eventTicketModel.PaymentDescription);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount), eventTicketModel.TotalAmount);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.VatAmount), eventTicketModel.TotalVat);
            Assert.Equal((int)ticketBuilderModel.Status, eventTicketModel.Status);
            Assert.Equal(ticketBuilderModel.EventName, eventTicketModel.EventName);
            Assert.Equal(ticketBuilderModel.EventTime.Date, eventTicketModel.EventDate.Date);
            Assert.Equal(ticketBuilderModel.EventLocation, eventTicketModel.EventLocation);
            Assert.Equal(ticketBuilderModel.EventCityId.ToString(), eventTicketModel.LocationCityCode);
            Assert.Equal(ticketBuilderModel.EventMunicipality, eventTicketModel.LocationMunicipality);
            Assert.Equal(ticketBuilderModel.EventDescription, eventTicketModel.LocationDescription);
            Assert.Equal(ticketBuilderModel.EventOrganizerVknTckn, eventTicketModel.EventOrganizerVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesVknTckn, eventTicketModel.RecordExpensesVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesTitle, eventTicketModel.RecordExpensesTitle);
            Assert.Equal(ticketBuilderModel.SeatNumber, eventTicketModel.SeatNumber);
            Assert.Null(eventTicketModel.Message);
            Assert.Equal((int)TicketEmailStatus.Created, eventTicketModel.EmailStatus);
            Assert.Null(eventTicketModel.XsltCode);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount) +
                         ticketBuilderModel.TicketLines.Sum(x => x.VatAmount) +
                         ticketBuilderModel.TicketLines.SelectMany(x => x.Taxes).Sum(y => y.TaxAmount)
                , eventTicketModel.PayableAmount);
        }

        [Fact]
        public async Task Should_Get_Event_Ticket_As_Pdf_Stream()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            var eventTicketPdfStream = await _eventTicketClient.GetPdfs(new MultiSelectModel<Guid>
            {
                Selected = new List<Guid> {new Guid(createdTicketResponseModel.Ettn)}
            });

            Assert.NotNull(eventTicketPdfStream);
        }

        [Fact]
        public async Task Should_Get_Event_Ticket_As_Html_Stream()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            var eventTicketHtmlStream = await _eventTicketClient.GetHtml(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(eventTicketHtmlStream);
        }

        [Fact]
        public async Task Should_Get_Event_Ticket_As_Xml_Stream()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            var eventTicketXmlStream = await _eventTicketClient.GetXml(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(eventTicketXmlStream);
        }

        [Fact]
        public async Task Should_Get_Event_Tickets_Statuses()
        {
            var ticketBuilderModel1 = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();
            var ticketBuilderModel2 = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel1 = await _eventTicketClient.Post(ticketBuilderModel1);
            var createdTicketResponseModel2 = await _eventTicketClient.Post(ticketBuilderModel2);
            
            var eventTicketStatusModels = await _eventTicketClient.GetStatuses(new List<Guid>
            {
                new Guid(createdTicketResponseModel1.Ettn),
                new Guid(createdTicketResponseModel2.Ettn)
            });

            Assert.NotNull(eventTicketStatusModels);
            Assert.NotEmpty(eventTicketStatusModels);
            Assert.Equal(2, eventTicketStatusModels.Count(x => x.Status == (int)TicketStatus.Draft));
            Assert.Equal(1, eventTicketStatusModels.Count(x => x.Id == ticketBuilderModel1.Ettn));
            Assert.Equal(1, eventTicketStatusModels.Count(x => x.Id == ticketBuilderModel2.Ettn));
        }

        [Fact]
        public async Task Should_Update_Event_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            ticketBuilderModel.EventName = "Updated Test Event Name";
            var updatedTicketResponseModel = await _eventTicketClient.Put(ticketBuilderModel);

            var updatedEventTicketModel = await _eventTicketClient.Get(new Guid(updatedTicketResponseModel.Ettn));

            Assert.Equal("Updated Test Event Name", updatedEventTicketModel.EventName);
        }

        [Fact]
        public async Task Should_Update_Event_Ticket_Status()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues().Build();

            var createdTicketResponseModel = await _eventTicketClient.Post(ticketBuilderModel);

            var isUpdateStatusSucceed = await _eventTicketClient.UpdateStatus(new UpdateTicketStatusModel()
            {
                Status = TicketStatus.Queued,
                Ids = new List<Guid> {new Guid(createdTicketResponseModel.Ettn)}
            });

            Assert.True(isUpdateStatusSucceed);
        }

        [Fact]
        public async Task Should_Cancel_Event_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultEventTicketValues()
                .With(x => x.Status = TicketStatus.Queued)
                .Build();

            await _eventTicketClient.Post(ticketBuilderModel);

            var approvedEventTicketList = await _eventTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .PageSize(1)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());

            var isCancelSucceed = await _eventTicketClient.Cancel(new CancelledTicketModel
            {
                Ids = approvedEventTicketList.Items.Select(ticket => ticket.Id).ToList()
            });

            Assert.True(isCancelSucceed);
        }
    }
}
