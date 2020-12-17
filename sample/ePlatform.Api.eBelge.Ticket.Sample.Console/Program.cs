using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.Core.Auth;
using ePlatform.Api.Core.Http;
using ePlatform.Api.eBelge.Ticket.Common;
using ePlatform.Api.eBelge.Ticket.Common.Enums;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.EventTicket;
using ePlatform.Api.eBelge.Ticket.EventTicket.Models;
using ePlatform.Api.eBelge.Ticket.PassengerTicket;
using ePlatform.Api.eBelge.Ticket.PassengerTicket.Models;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ePlatform.Api.eBelge.Ticket.Sample.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var clientOptions = new ClientOptions
            {
                AuthServiceUrl = "https://coretest.isim360.com",
                TicketServiceUrl = "https://ebiletservicetest.isim360.com",
                Auth = new ClientOptions.AuthOption
                {
                    ClientId = "serviceApi",
                    Username = "serviceuser01@isim360.com",
                    Password = "ePlatform123+"
                }
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //If you are using a single service, then you can use MemoryDistributedCache. Otherwise, you should use Redis etc.
            var distributedCacheProvider = new MemoryDistributedCache(
                new OptionsWrapper<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions()));
            var authClient = new AuthClient(clientOptions, client, distributedCacheProvider);

            FlurlHttp.Configure(settings => settings.HttpClientFactory = new PollyHttpClientFactory(authClient));
            var clientFactory = new PerBaseUrlFlurlClientFactory();

            var eventTicketClient = new EventTicketClient(clientOptions, clientFactory);
            await EvenTicketClientSampleOperations(eventTicketClient);

            var passengerTicketClient = new PassengerTicketClient(clientOptions, clientFactory);
            await PassengerTicketClientSampleOperations(passengerTicketClient);

            var commonEventTicketClient = new CommonTicketClient(clientOptions, clientFactory);

            //Get city list
            var cityList = await commonEventTicketClient.GetCityList();
        }

        private static async Task EvenTicketClientSampleOperations(EventTicketClient eventTicketClient)
        {
            //Create a new event ticket
            var model = CreateSampleEventTicketModel(TicketStatus.Draft);
            var draftEventTicket = await eventTicketClient.Post(model);

            // Get an event ticket
            var eventTicket = await eventTicketClient.Get(new Guid(draftEventTicket.Ettn));

            // Get an event detail
            var eventTicketDetail = await eventTicketClient.GetDetail(new Guid(draftEventTicket.Ettn));

            //Get a paginated event ticket list
            var paginatedEventTicketList = await eventTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .PageIndex(2)
                    .PageSize(100)
                    .QueryFor(x => x.IsArchived, Operator.Equal, false)
                    .Build());

            //Get an event ticket Pdf stream
            var eventTicketPdfStream = await eventTicketClient.GetPdfs(new MultiSelectModel<Guid>
            {
                Selected = new List<Guid> {new Guid(draftEventTicket.Ettn)}
            });

            //Get an event ticket Html stream
            var eventTicketHtmlStream = await eventTicketClient.GetHtml(new Guid(draftEventTicket.Ettn));

            //Get an event ticket Xml stream
            var eventTicketXmlStream = await eventTicketClient.GetXml(new Guid(draftEventTicket.Ettn));

            //Get event tickets statuses
            var eventTicketsStatuses = await eventTicketClient.GetStatuses(new List<Guid> {new Guid(draftEventTicket.Ettn)});

            //Update an event ticket
            model.CustomerCity = "Updated Test City";
            var updatedEventTicket = await eventTicketClient.Put(model);

            //Update the status of event tickets
            var isUpdateStatusSucceed = await eventTicketClient.UpdateStatus(new UpdateTicketStatusModel()
            {
                Status = TicketStatus.Queued,
                Ids = new List<Guid> {new Guid(draftEventTicket.Ettn)}
            });

            //Cancel an event ticket
            var approvedEventTicketList = await eventTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .PageSize(1)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());
            var isCancelSucceed = await eventTicketClient.Cancel(new CancelledTicketModel
            {
                Ids = approvedEventTicketList.Items.Select(ticket => ticket.Id).ToList()
            });
        }
        private static async Task PassengerTicketClientSampleOperations(PassengerTicketClient passengerTicketClient)
        {
            //Create a new passenger ticket
            var model = CreateSamplePassengerTicketModel(TicketStatus.Draft);
            var draftPassengerTicket = await passengerTicketClient.Post(model);

            // Get an passenger ticket
            var passengerTicket = await passengerTicketClient.Get(new Guid(draftPassengerTicket.Ettn));

            // Get an passenger detail
            var passengerTicketDetail = await passengerTicketClient.GetDetail(new Guid(draftPassengerTicket.Ettn));

            //Get a paginated passenger ticket list
            var paginatedPassengerTicketList = await passengerTicketClient.GetTicketList(
                new QueryFilterBuilder<PassengerTicketModel>()
                    .PageIndex(2)
                    .PageSize(100)
                    .QueryFor(x => x.IsArchived, Operator.Equal, false)
                    .Build());

            //Get an passenger ticket Pdf stream
            var passengerTicketPdfStream = await passengerTicketClient.GetPdfs(new MultiSelectModel<Guid>
            {
                Selected = new List<Guid> {new Guid(draftPassengerTicket.Ettn)}
            });

            //Get an passenger ticket Html stream
            var passengerTicketHtmlStream = await passengerTicketClient.GetHtml(new Guid(draftPassengerTicket.Ettn));

            //Get an passenger ticket Xml stream
            var passengerTicketXmlStream = await passengerTicketClient.GetXml(new Guid(draftPassengerTicket.Ettn));

            //Get passenger tickets statuses
            var passengerTicketStatuses = await passengerTicketClient.GetStatuses(new List<Guid> {new Guid(draftPassengerTicket.Ettn)});

            //Update an passenger ticket
            model.CustomerCity = "Updated Test City";
            var updatedPassengerTicket = await passengerTicketClient.Put(model);

            //Update the status of passenger tickets
            var isUpdateStatusSucceed = await passengerTicketClient.UpdateStatus(new UpdateTicketStatusModel()
            {
                Status = TicketStatus.Queued,
                Ids = new List<Guid> {new Guid(draftPassengerTicket.Ettn)}
            });

            //Cancel an passenger ticket
            var approvedPassengerTicketList = await passengerTicketClient.GetTicketList(
                new QueryFilterBuilder<PassengerTicketModel>()
                    .PageSize(1)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());
            var isCancelSucceed = await passengerTicketClient.Cancel(new CancelledTicketModel
            {
                Ids = approvedPassengerTicketList.Items.Select(ticket => ticket.Id).ToList()
            });
        }
        private static TicketBuilderModel CreateSampleEventTicketModel(TicketStatus status)
        {
            var eventTicket = CreateSampleTicketModel(status);
            eventTicket.TicketType = TicketType.ETKINLIK;
            eventTicket.EventTime = DateTime.Now.AddDays(2);
            eventTicket.EventName = "Tarkan Konseri - 21";
            eventTicket.EventLocation = "Harbiye Açık Hava 2021";
            eventTicket.EventCity = "İstanbul";
            eventTicket.EventCityId = 80;
            eventTicket.EventMunicipality = "Beyoğlu";
            eventTicket.EventDescription = "Tarkan - Harbiye Açık Hava Konseri 2021";
            eventTicket.EventOrganizerVknTckn = "12345678901";
            return eventTicket;
        }
        private static TicketBuilderModel CreateSamplePassengerTicketModel(TicketStatus status)
        {
            var passengerTicket = CreateSampleTicketModel(status);
            passengerTicket.TicketType = TicketType.YOLCU;
            passengerTicket.VehiclePlate = "34ABC34";
            passengerTicket.DepartureDate = DateTime.Now.AddDays(2);
            passengerTicket.ExpeditionTime = DateTime.Now.AddDays(2);
            passengerTicket.ExpeditionNumber = "123";
            passengerTicket.DepartureLocation = "Sakarya";
            passengerTicket.VehicleOperatingTitle = "Test";
            passengerTicket.VehicleOperatingVknTckn = "12345678901";
            passengerTicket.VehicleOperatingTaxCenter = "Test";
            return passengerTicket;
        }
        private static TicketBuilderModel CreateSampleTicketModel(TicketStatus status)
        {
            return new TicketBuilderModel
            {
                Ettn = Guid.NewGuid(),
                Status = status,
                TicketNumber = $"ABC{DateTime.Now.Year}{new Random().Next(100000000, 999999999)}",
                Prefix = "ABC",
                ReferenceNumber = "RFR1344",
                TicketDate = DateTime.Now,
                DocumentType = DocumentType.SATIS,
                CurrencyCode = Currency.TRY,
                Identifier = "273378723",
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                CustomerStreet = "a",
                CustomerBuildingName = "a",
                CustomerBuildingNo = "a",
                CustomerDoorNo = "a",
                CustomerTown = "a",
                CustomerDistrict = "a",
                CustomerCity = "a",
                CustomerTelephone = "5367766470",
                CustomerEmail = "johndoe@eplatform.com.tr",
                IsEmailSend = true,
                PaymentType = PaymentType.DIGER,
                PaymentDescription = "aa",
                SeatNumber = "A34",
                CommissionAmount = 10,
                CommissionTaxAmount = 10,
                TicketLines = new List<TicketLine>
                {
                    new TicketLine
                    {
                        ServiceType = ServiceType.DIGER,
                        ServiceDescription = "Bilet Bedeli + Hizmet Bedeli",
                        Amount = 100,
                        DiscountRate = 10,
                        DiscountAmount = 10,
                        VatRate = 18,
                        VatAmount = 16.2m,
                        Taxes = new List<TaxModel>
                        {
                            new TaxModel
                            {
                                TaxCode = "0030",
                                TaxName = "Özel vergi",
                                TaxRate = 20,
                                TaxAmount = 100
                            },
                            new TaxModel
                            {
                                TaxCode = "0030",
                                TaxName = "Özel vergi",
                                TaxRate = 20,
                                TaxAmount = 100
                            }
                        }
                    }
                },
                Notes = new List<NoteModel>
                {
                    new NoteModel {Note = "örnek not 1"},
                    new NoteModel {Note = "örnek not 2"}
                }
            };
        }
    }
}
