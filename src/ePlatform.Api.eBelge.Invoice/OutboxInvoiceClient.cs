using ePlatform.Api.Core;
using ePlatform.Api.Core.Http;
using ePlatform.Api.eBelge.Invoice.Models;
using Flurl.Http;
using Flurl.Http.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ePlatform.Api.eBelge.Invoice
{
    public class OutboxInvoiceClient
    {
        private readonly IFlurlClient flurlClient;

        public UblBuilderModel NewOutboxInvoceModel()
        {
            return new UblBuilderModel()
            {
                RecordType = (int)RecordType.Invoice,
            };
        }

        public InvoiceLineBaseModel<InvoiceLineTaxBaseModel> NewInvoceLineModel()
        {
            return new InvoiceLineBaseModel<InvoiceLineTaxBaseModel>();
        }

        public OutboxInvoiceClient(ClientOptions clientOptions, IFlurlClientFactory flurlClientFac)
        {
            flurlClient = flurlClientFac.Get(clientOptions.InvoiceServiceUrl).SetDefaultSettings();
            flurlClient.Headers["Client-Version"] = Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// Faturanın özet bilgilerini çeker
        /// </summary>
        public async Task<OutboxInvoiceGetModel> Get(Guid id)
        {
            return await flurlClient.Request($"/v1/outboxinvoice/{id}")
                .GetJsonAsync<OutboxInvoiceGetModel>();
        }

        /// <summary>
        /// Faturanın UBLModel halini çeker.
        /// </summary>
        public async Task<UblBuilderModel> GetInvoice(Guid id)
        {
            return await flurlClient.Request($"/v1/outboxinvoice/getinvoice")
                .SetQueryParam("guid", id)
                .GetJsonAsync<UblBuilderModel>();
        }

        /// <summary>
        /// Get Filtered Outbox Invoice
        /// </summary>
        public async Task<PagedList<OutboxInvoiceGetModel>> GetList(PagingModel model)
        {
            return await flurlClient.Request($"/v1/outboxinvoice/list")
                .SetQueryParams(model)
                .GetJsonAsync<PagedList<OutboxInvoiceGetModel>>();
        }

        public async Task<CreateInvoiceResponseModel> Post(UblBuilderModel model)
        {
            return await flurlClient.Request($"/v1/outboxinvoice/create")
                .PostJsonAsync(model)
                .ReceiveJson<CreateInvoiceResponseModel>();
        }

        public async Task<CreateInvoiceResponseModel> Update(Guid id, UblBuilderModel model)
        {
            return await flurlClient.Request($"/v1/outboxinvoice/update/{id.ToString()}")
                .PutJsonAsync(model)
                .ReceiveJson<CreateInvoiceResponseModel>();
        }

        public async Task<CreateInvoiceResponseModel> PostUBL(CreateInvoiceModel model)
        {
            return await flurlClient.Request($"/v1/outboxinvoice")
                .PostJsonAsync(model)
                .ReceiveJson<CreateInvoiceResponseModel>();
        }

        public async Task<CreateInvoiceResponseModel> UpdateUBL(CreateInvoiceModel model)
        {
            return await flurlClient.Request($"/v1/outboxinvoice")
                .PutJsonAsync(model)
                .ReceiveJson<CreateInvoiceResponseModel>();
        }

        public async Task<bool> UpdateStatusList(UpdateInvoiceModel model)
        {
            var response = await flurlClient.Request($"/v1/outboxinvoice/updatestatuslist")
                .PutJsonAsync(model);
            return response.IsSuccessStatusCode;
        }
        

        public async Task<Stream> GetUbl(Guid id)
        {
            return await flurlClient.Request($"/v2/outboxinvoice/{id}/ubl")
                .GetStreamAsync();
        }

        public async Task<Stream> GetPdf(Guid id, bool useStandartXslt = false)
        {
            return await flurlClient.Request($"/v2/outboxinvoice/{id}/pdf/{useStandartXslt}")
                .GetStreamAsync();
        }

        /// <summary>
        /// Gets a ZIP of the outbox invoice with the given id value as a <see cref="Stream"/>.
        /// </summary>
        /// <param name="id">The id of the outbox invoice</param>
        /// <param name="useStandardXslt">If this field is sent as false, then the invoice is returned according to the
        /// default one among the XSLTs you have added. Otherwise, the standard XSLT ticket is returned.</param>
        /// <returns>The task result contains the <see cref="Stream"/> object.</returns>
        public async Task<Stream> GetZip(Guid id, bool useStandardXslt = false)
        {
            return await flurlClient.Request($"/v2/outboxinvoice/{id}/zip/{useStandardXslt}")
                .GetStreamAsync();
        }

        public async Task<Stream> GetHtml(Guid id, bool useStandartXslt = false)
        {
            return await flurlClient.Request($"/v2/outboxinvoice/{id}/html/{useStandartXslt}")
                .GetStreamAsync();
        }
        
        public async Task<ApproveRejectInvoiceModel> GetInvoiceResponse(Guid id)
        {
            return await flurlClient.Request($"/v1/invoiceresponse/getbyinvoiceid/{id}")
                .GetJsonAsync<ApproveRejectInvoiceModel>();
        }

        /// <summary>
        /// Gets the status of your outbox invoice on GIB side.
        /// For e-Archive invoices, envelope information is not created.
        /// </summary>
        /// <param name="id">The id of the invoice</param>
        /// <param name="isGibStatus">You must send this field as true to see the GIB status information.</param>
        /// <returns>The task result contains the <see cref="OutboxEnvelopeModel"/> object.</returns>
        public async Task<OutboxEnvelopeModel> GetInvoiceEnvelope(Guid id, bool isGibStatus)
        {
            return await flurlClient.Request($"/v1/outboxenvelope")
                .SetQueryParams(new
                {
                    id,
                    isGibStatus
                })
                .GetJsonAsync<OutboxEnvelopeModel>();
        }
    }
}
