using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.Core.Http;
using ePlatform.Api.eBelge.Ticket.Common.Enums;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.EventTicket.Models;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace ePlatform.Api.eBelge.Ticket.EventTicket
{
    public class EventTicketClient
    {
        private readonly IFlurlClient flurlClient;
        public EventTicketClient(ClientOptions clientOptions, IFlurlClientFactory flurlClientFactory)
        {
            flurlClient = flurlClientFactory.Get(clientOptions.TicketServiceUrl).SetDefaultSettings();
            flurlClient.Headers["Client-Version"] = Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// Gets a <see cref="EventTicketModel"/> with the given id value.
        /// </summary>
        /// <param name="id">The id of the event ticket.</param>
        /// <returns> The task result contains the <see cref="EventTicketModel"/> object.</returns>
        public async Task<EventTicketModel> Get(Guid id)
        {
            return await flurlClient.Request($"/v1/event-ticket/{id}")
                .GetJsonAsync<EventTicketModel>();
        }

        /// <summary>
        /// Gets a <see cref="TicketBuilderModel"/> with the given id value.
        /// </summary>
        /// <param name="id">The id of the event ticket.</param>
        /// <returns> The task result contains the <see cref="TicketBuilderModel"/> object.</returns>
        public async Task<TicketBuilderModel> GetDetail(Guid id)
        {
            return await flurlClient.Request($"/v1/event-ticket/detail/{id}")
                .GetJsonAsync<TicketBuilderModel>();
        }

        /// <summary>
        /// Gets a <see cref="PagedList{EventTicketModel}"/>  for a paginated result.
        /// </summary>
        /// <param name="model">Model for the necessary parameters to execute service method.</param>
        /// <returns>The task result contains the <see cref="PagedList{EventTicketModel}"/> object.</returns>
        public async Task<PagedList<EventTicketModel>> GetTicketList(PagingModel model)
        {
            return await flurlClient.Request("/v1/event-ticket")
                .SetQueryParams(model)
                .GetJsonAsync<PagedList<EventTicketModel>>();
        }

        /// <summary>
        /// Gets PDFs of the tickets with the given selected event ticket ids as a <see cref="Stream"/>.
        /// If <paramref name="model"/> contains more than one id it returns <see cref="Stream"/> as zip format, otherwise pdf format.
        /// </summary>
        /// <param name="model">Model for the selected event ticket ids.</param>
        /// <param name="isStandardXslt">If this field is sent as false, then the ticket is returned according to the
        /// default one among the XSLTs you have added. Otherwise, the standard XSLT ticket is returned.</param>
        /// <returns>The task result contains the <see cref="Stream"/> object.</returns>
        public async Task<Stream> GetPdfs(MultiSelectModel<Guid> model, bool isStandardXslt = false)
        {
            return await flurlClient.Request($"/v1/event-ticket/pdf/{isStandardXslt}")
                .PostJsonAsync(model)
                .ReceiveStream();
        }

        /// <summary>
        /// Gets a HTML of the ticket with the given id value as a <see cref="Stream"/>.
        /// </summary>
        /// <param name="id">The id of the event ticket</param>
        /// <param name="isStandardXslt">If this field is sent as false, then the ticket is returned according to the
        /// default one among the XSLTs you have added. Otherwise, the standard XSLT ticket is returned.</param>
        /// <returns>The task result contains the <see cref="Stream"/> object.</returns>
        public async Task<Stream> GetHtml(Guid id, bool isStandardXslt = false)
        {
            return await flurlClient.Request($"/v1/event-ticket/{id}/html/{isStandardXslt}")
                .GetStreamAsync();
        }

        /// <summary>
        /// Gets a XML of the ticket with the given id value as a <see cref="Stream"/>.
        /// </summary>
        /// <param name="id">The id of the event ticket</param>
        /// <returns>The task result contains the <see cref="Stream"/> object.</returns>
        public async Task<Stream> GetXml(Guid id)
        {
            return await flurlClient.Request($"/v1/event-ticket/xml/{id}")
                .GetStreamAsync();
        }

        /// <summary>
        /// Gets a list of <see cref="EventTicketStatusModel"/> with the given ids
        /// </summary>
        /// <param name="ids">The ids of event tickets</param>
        /// <returns>The task result contains the list of <see cref="EventTicketStatusModel"/>.</returns>
        public async Task<List<EventTicketStatusModel>> GetStatuses(List<Guid> ids)
        {
            return await flurlClient.Request("/v1/event-ticket/status")
                .PostJsonAsync(ids)
                .ReceiveJson<List<EventTicketStatusModel>>();
        }

        /// <summary>
        /// Creates a new event ticket.
        /// </summary>
        /// <param name="model">Model for the necessary parameters to create a new event ticket.</param>
        /// <returns>The task result contains the <see cref="CreatedTicketResponseModel"/> object.</returns>
        public async Task<CreatedTicketResponseModel> Post(TicketBuilderModel model)
        {
            return await flurlClient.Request("/v1/event-ticket")
                .PostJsonAsync(model)
                .ReceiveJson<CreatedTicketResponseModel>();
        }

        /// <summary>
        /// Updates an event ticket that is in <see cref="TicketStatus.Error"/> or <see cref="TicketStatus.Draft"/> status.
        /// </summary>
        /// <param name="model">Model for the necessary parameters to update an event ticket.</param>
        /// <returns>The task result contains the <see cref="CreatedTicketResponseModel"/> object.</returns>
        public async Task<CreatedTicketResponseModel> Put(TicketBuilderModel model)
        {
            return await flurlClient.Request($"/v1/event-ticket/{model.Ettn}")
                .PutJsonAsync(model)
                .ReceiveJson<CreatedTicketResponseModel>();
        }

        /// <summary>
        /// Cancels event tickets.
        /// </summary>
        /// <param name="model">Model for the selected event ticket ids.</param>
        /// <returns>The task result contains the info about if the cancel operation succeeds or not.</returns>
        public async Task<bool> Cancel(CancelledTicketModel model)
        {
            var response = await flurlClient.Request("/v1/event-ticket/cancel")
                .PostJsonAsync(model);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Updates the status of event tickets that are in <see cref="TicketStatus.Error"/> or <see cref="TicketStatus.Draft"/> status.
        /// </summary>
        /// <param name="model">Model for the selected event ticket ids and their new status.</param>
        /// <returns>The task result contains the info about if the update status operation succeeds or not.</returns>
        public async Task<bool> UpdateStatus(UpdateTicketStatusModel model)
        {
            var response = await flurlClient.Request("/v1/event-ticket/updatestatuslist")
                .PutJsonAsync(model);
            return response.IsSuccessStatusCode;
        }
    }
}
