using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Ticket.Common.Enums;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.EventTicket.Models;
using ePlatform.Api.eBelge.Ticket.PassengerTicket;
using ePlatform.Api.eBelge.Ticket.Tests.Builders;
using ePlatform.Api.eBelge.Ticket.Tests.Setup;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ePlatform.Api.eBelge.Ticket.Tests.Functional
{
    [Collection("ticket-startup")]
    public class PassengerTicketTest
    {
        private readonly PassengerTicketClient _passengerTicketClient;
        private readonly TicketBuilderModelBuilder _ticketBuilderModelBuilder;

        public PassengerTicketTest(StartupFixture fixture)
        {
            _passengerTicketClient = fixture.ServiceProvider.GetRequiredService<PassengerTicketClient>();
            _ticketBuilderModelBuilder = fixture.ServiceProvider.GetRequiredService<TicketBuilderModelBuilder>();
        }

        [Fact]
        public async Task Should_Create_Passenger_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();
            
            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            Assert.NotNull(createdTicketResponseModel);
            Assert.NotNull(createdTicketResponseModel.Ettn);
            Assert.NotNull(createdTicketResponseModel.TicketNumber);
            Assert.Equal(ticketBuilderModel.Ettn, new Guid(createdTicketResponseModel.Ettn));
            Assert.Equal(ticketBuilderModel.TicketNumber, createdTicketResponseModel.TicketNumber);
        }

        [Fact]
        public async Task Should_Get_Passenger_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            var passengerTicketModel = await _passengerTicketClient.Get(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(passengerTicketModel);
            Assert.Equal(ticketBuilderModel.Ettn, passengerTicketModel.Id);
            Assert.Equal(ticketBuilderModel.TicketNumber, passengerTicketModel.TicketNumber);
            Assert.Equal($"{ticketBuilderModel.CustomerFirstName} {ticketBuilderModel.CustomerLastName}", passengerTicketModel.TargetTitle);
            Assert.Equal(ticketBuilderModel.Identifier, passengerTicketModel.TargetVknTckn);
            Assert.NotNull(passengerTicketModel.ExecutionDate);
            Assert.Equal(ticketBuilderModel.ReferenceNumber,passengerTicketModel.LocalReferenceId);
            Assert.Equal(Currency.TRY.ToString(), passengerTicketModel.Currency);
            Assert.Equal(ticketBuilderModel.ExchangeRate, passengerTicketModel.CurrencyRate);
            Assert.Equal((int)ticketBuilderModel.DocumentType, passengerTicketModel.DocumentType);
            Assert.Equal(ticketBuilderModel.PaymentType.ToString(), passengerTicketModel.PaymentType);
            Assert.Equal(ticketBuilderModel.PaymentDescription, passengerTicketModel.PaymentDescription);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount), passengerTicketModel.TotalAmount);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.VatAmount), passengerTicketModel.TotalVat);
            Assert.Equal((int)ticketBuilderModel.Status, passengerTicketModel.Status);
            Assert.Equal(ticketBuilderModel.DepartureDate.AddTicks(-ticketBuilderModel.DepartureDate.Ticks % TimeSpan.TicksPerSecond),
                passengerTicketModel.DepartureDate.AddTicks(-passengerTicketModel.DepartureDate.Ticks % TimeSpan.TicksPerSecond));
            Assert.Equal(ticketBuilderModel.DepartureLocation, passengerTicketModel.DepartureLocation);
            Assert.Equal(ticketBuilderModel.ExpeditionTime.AddTicks(-ticketBuilderModel.ExpeditionTime.Ticks % TimeSpan.TicksPerSecond),
                passengerTicketModel.ExpeditionTime.AddTicks(-passengerTicketModel.ExpeditionTime.Ticks % TimeSpan.TicksPerSecond));
            Assert.Equal(ticketBuilderModel.ExpeditionNumber, passengerTicketModel.ExpeditionNumber);
            Assert.Equal(ticketBuilderModel.VehicleOperatingVknTckn, passengerTicketModel.VehicleOperatingVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesVknTckn, passengerTicketModel.RecordExpensesVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesTitle, passengerTicketModel.RecordExpensesTitle);
            Assert.Equal(ticketBuilderModel.SeatNumber, passengerTicketModel.SeatNumber);
            Assert.Null(passengerTicketModel.Message);
            Assert.Equal((int)TicketEmailStatus.Created, passengerTicketModel.EmailStatus);
            Assert.Null(passengerTicketModel.XsltCode);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount) +
                         ticketBuilderModel.TicketLines.Sum(x => x.VatAmount) +
                         ticketBuilderModel.TicketLines.SelectMany(x => x.Taxes).Sum(y => y.TaxAmount)
                , passengerTicketModel.PayableAmount);
        }

        [Fact]
        public async Task Should_Get_Passenger_Ticket_Detail()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            var detailedPassengerTicket = await _passengerTicketClient.GetDetail(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(detailedPassengerTicket);
            Assert.Equal(ticketBuilderModel.Ettn, detailedPassengerTicket.Ettn);
            Assert.Equal(ticketBuilderModel.Status, detailedPassengerTicket.Status);
            Assert.Equal(ticketBuilderModel.TicketNumber, detailedPassengerTicket.TicketNumber);
            Assert.Equal(ticketBuilderModel.Prefix, detailedPassengerTicket.Prefix);
            Assert.Equal(ticketBuilderModel.ReferenceNumber, detailedPassengerTicket.ReferenceNumber);
            Assert.Equal(ticketBuilderModel.TicketType, detailedPassengerTicket.TicketType);
            Assert.Equal(ticketBuilderModel.TicketDate.Date, detailedPassengerTicket.TicketDate.Date);
            Assert.Equal(ticketBuilderModel.DocumentType, detailedPassengerTicket.DocumentType);
            Assert.Equal(ticketBuilderModel.CurrencyCode, detailedPassengerTicket.CurrencyCode);
            Assert.Equal(ticketBuilderModel.ExchangeRate, detailedPassengerTicket.ExchangeRate);
            Assert.Equal(ticketBuilderModel.Identifier, detailedPassengerTicket.Identifier);
            Assert.Equal(ticketBuilderModel.CustomerFirstName, detailedPassengerTicket.CustomerFirstName);
            Assert.Equal(ticketBuilderModel.CustomerStreet, detailedPassengerTicket.CustomerStreet);
            Assert.Equal(ticketBuilderModel.CustomerBuildingName, detailedPassengerTicket.CustomerBuildingName);
            Assert.Equal(ticketBuilderModel.CustomerBuildingNo, detailedPassengerTicket.CustomerBuildingNo);
            Assert.Equal(ticketBuilderModel.CustomerDoorNo, detailedPassengerTicket.CustomerDoorNo);
            Assert.Equal(ticketBuilderModel.CustomerTown, detailedPassengerTicket.CustomerTown);
            Assert.Equal(ticketBuilderModel.CustomerDistrict, detailedPassengerTicket.CustomerDistrict);
            Assert.Equal(ticketBuilderModel.CustomerCity, detailedPassengerTicket.CustomerCity);
            Assert.Equal(ticketBuilderModel.CustomerCountry, detailedPassengerTicket.CustomerCountry);
            Assert.Equal(ticketBuilderModel.CustomerTelephone, detailedPassengerTicket.CustomerTelephone);
            Assert.Equal(ticketBuilderModel.CustomerEmail, detailedPassengerTicket.CustomerEmail);
            Assert.Equal(ticketBuilderModel.IsEmailSend, detailedPassengerTicket.IsEmailSend);
            Assert.Equal(ticketBuilderModel.CustomerTaxCenter, detailedPassengerTicket.CustomerTaxCenter);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount), detailedPassengerTicket.TotalAmount);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.VatAmount), detailedPassengerTicket.TotalVat);
            Assert.Equal(ticketBuilderModel.PaymentType, detailedPassengerTicket.PaymentType);
            Assert.Equal(ticketBuilderModel.PaymentDescription, detailedPassengerTicket.PaymentDescription);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount) +
                         ticketBuilderModel.TicketLines.Sum(x => x.VatAmount) +
                         ticketBuilderModel.TicketLines.SelectMany(x => x.Taxes).Sum(y => y.TaxAmount)
                , detailedPassengerTicket.PayableAmount);
            Assert.Equal(ticketBuilderModel.VehiclePlate, detailedPassengerTicket.VehiclePlate);
            Assert.Equal(ticketBuilderModel.ExpeditionTime, detailedPassengerTicket.ExpeditionTime);
            Assert.Equal(ticketBuilderModel.ExpeditionNumber, detailedPassengerTicket.ExpeditionNumber);
            Assert.Equal(ticketBuilderModel.DepartureDate, detailedPassengerTicket.DepartureDate);
            Assert.Equal(ticketBuilderModel.DepartureLocation, detailedPassengerTicket.DepartureLocation);
            Assert.Equal(ticketBuilderModel.SeatNumber, detailedPassengerTicket.SeatNumber);
            Assert.Equal(ticketBuilderModel.VehicleOperatingVknTckn, detailedPassengerTicket.VehicleOperatingVknTckn);
            Assert.Equal(ticketBuilderModel.CommissionAmount, detailedPassengerTicket.CommissionAmount);
            Assert.Equal(ticketBuilderModel.CommissionTaxAmount, detailedPassengerTicket.CommissionTaxAmount);
            Assert.Equal(ticketBuilderModel.RecordExpensesVknTckn, detailedPassengerTicket.RecordExpensesVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesTitle, detailedPassengerTicket.RecordExpensesTitle);
            Assert.Equal(ticketBuilderModel.EventTime, detailedPassengerTicket.EventTime);
            Assert.Equal(ticketBuilderModel.EventName, detailedPassengerTicket.EventName);
            Assert.Equal(ticketBuilderModel.EventLocation, detailedPassengerTicket.EventLocation);
            Assert.Equal(ticketBuilderModel.EventCityId, detailedPassengerTicket.EventCityId);
            Assert.Equal(ticketBuilderModel.EventMunicipality, detailedPassengerTicket.EventMunicipality);
            Assert.Equal(ticketBuilderModel.EventDescription, detailedPassengerTicket.EventDescription);
            Assert.Equal(ticketBuilderModel.EventOrganizerVknTckn, detailedPassengerTicket.EventOrganizerVknTckn);
            Assert.Equal(ticketBuilderModel.XsltCode, detailedPassengerTicket.XsltCode);
        }

        [Fact]
        public async Task Should_Get_Passenger_Ticket_List()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            var paginatedPassengerTicketList = await _passengerTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .QueryFor(x => x.Id, Operator.Equal, createdTicketResponseModel.Ettn)
                    .Build());
            var passengerTicketModel = paginatedPassengerTicketList.Items?.First();

            Assert.NotNull(paginatedPassengerTicketList);
            Assert.NotNull(passengerTicketModel);
            Assert.NotNull(paginatedPassengerTicketList.Items);
            Assert.NotEmpty(paginatedPassengerTicketList.Items);
            Assert.False(paginatedPassengerTicketList.HasNextPage);
            Assert.True(paginatedPassengerTicketList.HasPreviousPage);
            Assert.Equal(1, paginatedPassengerTicketList.PageIndex);
            Assert.Equal(50, paginatedPassengerTicketList.PageSize);
            Assert.Equal(1, paginatedPassengerTicketList.TotalCount);
            Assert.Equal(1, paginatedPassengerTicketList.TotalPages);
            Assert.Equal(ticketBuilderModel.Ettn, passengerTicketModel.Id);
            Assert.Equal(ticketBuilderModel.Ettn, passengerTicketModel.Id);
            Assert.Equal(ticketBuilderModel.TicketNumber, passengerTicketModel.TicketNumber);
            Assert.Equal($"{ticketBuilderModel.CustomerFirstName} {ticketBuilderModel.CustomerLastName}", passengerTicketModel.TargetTitle);
            Assert.Equal(ticketBuilderModel.Identifier, passengerTicketModel.TargetVknTckn);
            Assert.NotNull(passengerTicketModel.ExecutionDate);
            Assert.Equal(ticketBuilderModel.ReferenceNumber,passengerTicketModel.LocalReferenceId);
            Assert.Equal(Currency.TRY.ToString(), passengerTicketModel.Currency);
            Assert.Equal(ticketBuilderModel.ExchangeRate, passengerTicketModel.CurrencyRate);
            Assert.Equal((int)ticketBuilderModel.DocumentType, passengerTicketModel.DocumentType);
            Assert.Equal(ticketBuilderModel.PaymentType.ToString(), passengerTicketModel.PaymentType);
            Assert.Equal(ticketBuilderModel.PaymentDescription, passengerTicketModel.PaymentDescription);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount), passengerTicketModel.TotalAmount);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.VatAmount), passengerTicketModel.TotalVat);
            Assert.Equal((int)ticketBuilderModel.Status, passengerTicketModel.Status);
            Assert.Equal(ticketBuilderModel.DepartureDate.AddTicks(-ticketBuilderModel.DepartureDate.Ticks % TimeSpan.TicksPerSecond),
                passengerTicketModel.DepartureDate.AddTicks(-passengerTicketModel.DepartureDate.Ticks % TimeSpan.TicksPerSecond));
            Assert.Equal(ticketBuilderModel.DepartureLocation, passengerTicketModel.DepartureLocation);
            Assert.Equal(ticketBuilderModel.ExpeditionTime.AddTicks(-ticketBuilderModel.ExpeditionTime.Ticks % TimeSpan.TicksPerSecond),
                passengerTicketModel.ExpeditionTime.AddTicks(-passengerTicketModel.ExpeditionTime.Ticks % TimeSpan.TicksPerSecond));
            Assert.Equal(ticketBuilderModel.ExpeditionNumber, passengerTicketModel.ExpeditionNumber);
            Assert.Equal(ticketBuilderModel.VehicleOperatingVknTckn, passengerTicketModel.VehicleOperatingVknTckn);
            Assert.Equal(ticketBuilderModel.VehiclePlate, passengerTicketModel.VehiclePlate);
            Assert.Equal(ticketBuilderModel.RecordExpensesVknTckn, passengerTicketModel.RecordExpensesVknTckn);
            Assert.Equal(ticketBuilderModel.RecordExpensesTitle, passengerTicketModel.RecordExpensesTitle);
            Assert.Equal(ticketBuilderModel.SeatNumber, passengerTicketModel.SeatNumber);
            Assert.Null(passengerTicketModel.Message);
            Assert.Equal((int)TicketEmailStatus.Created, passengerTicketModel.EmailStatus);
            Assert.Null(passengerTicketModel.XsltCode);
            Assert.Equal(ticketBuilderModel.TicketLines.Sum(x => x.Amount - x.DiscountAmount) +
                         ticketBuilderModel.TicketLines.Sum(x => x.VatAmount) +
                         ticketBuilderModel.TicketLines.SelectMany(x => x.Taxes).Sum(y => y.TaxAmount)
                , passengerTicketModel.PayableAmount);
        }

        [Fact]
        public async Task Should_Get_Passenger_Ticket_As_Pdf_Stream()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            var passengerTicketPdfStream = await _passengerTicketClient.GetPdfs(new MultiSelectModel<Guid>
            {
                Selected = new List<Guid> {new Guid(createdTicketResponseModel.Ettn)}
            });

            Assert.NotNull(passengerTicketPdfStream);
        }

        [Fact]
        public async Task Should_Get_Passenger_Ticket_As_Html_Stream()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            var passengerTicketHtmlStream = await _passengerTicketClient.GetHtml(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(passengerTicketHtmlStream);
        }

        [Fact]
        public async Task Should_Get_Passenger_Ticket_As_Xml_Stream()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            var passengerTicketXmlStream = await _passengerTicketClient.GetXml(new Guid(createdTicketResponseModel.Ettn));

            Assert.NotNull(passengerTicketXmlStream);
        }
        
        [Fact]
        public async Task Should_Get_Passenger_Tickets_Statuses()
        {
            var ticketBuilderModel1 = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();
            var ticketBuilderModel2 = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel1 = await _passengerTicketClient.Post(ticketBuilderModel1);
            var createdTicketResponseModel2 = await _passengerTicketClient.Post(ticketBuilderModel2);
            
            var passengerTicketStatusModels = await _passengerTicketClient.GetStatuses(new List<Guid>
            {
                new Guid(createdTicketResponseModel1.Ettn),
                new Guid(createdTicketResponseModel2.Ettn)
            });

            Assert.NotNull(passengerTicketStatusModels);
            Assert.NotEmpty(passengerTicketStatusModels);
            Assert.Equal(2, passengerTicketStatusModels.Count(x => x.Status == (int)TicketStatus.Draft));
            Assert.Equal(1, passengerTicketStatusModels.Count(x => x.Id == ticketBuilderModel1.Ettn));
            Assert.Equal(1, passengerTicketStatusModels.Count(x => x.Id == ticketBuilderModel2.Ettn));
        }
        
        [Fact]
        public async Task Should_Update_Passenger_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            ticketBuilderModel.DepartureLocation = "Updated Test Departure Location";
            var updatedTicketResponseModel = await _passengerTicketClient.Put(ticketBuilderModel);

            var passengerTicketModel = await _passengerTicketClient.Get(new Guid(updatedTicketResponseModel.Ettn));

            Assert.Equal("Updated Test Departure Location", passengerTicketModel.DepartureLocation);
        }

        [Fact]
        public async Task Should_Update_Passenger_Ticket_Status()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues().Build();

            var createdTicketResponseModel = await _passengerTicketClient.Post(ticketBuilderModel);

            var isUpdateStatusSucceed = await _passengerTicketClient.UpdateStatus(new UpdateTicketStatusModel()
            {
                Status = TicketStatus.Queued,
                Ids = new List<Guid> {new Guid(createdTicketResponseModel.Ettn)}
            });

            Assert.True(isUpdateStatusSucceed);
        }

        [Fact]
        public async Task Should_Cancel_Passenger_Ticket()
        {
            var ticketBuilderModel = _ticketBuilderModelBuilder.CreateWithDefaultPassengerTicketValues()
                .With(x => x.Status = TicketStatus.Queued)
                .Build();

            await _passengerTicketClient.Post(ticketBuilderModel);

            var approvedPassengerTicketList = await _passengerTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .PageSize(1)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());

            var isCancelSucceed = await _passengerTicketClient.Cancel(new CancelledTicketModel
            {
                Ids = approvedPassengerTicketList.Items.Select(ticket => ticket.Id).ToList()
            });

            Assert.True(isCancelSucceed);
        }
    }
}
