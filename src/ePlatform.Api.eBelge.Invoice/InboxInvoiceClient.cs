using ePlatform.Api.Core.Http;
using ePlatform.Api.Core;
using Flurl.Http;
using Flurl.Http.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ePlatform.Api.eBelge.Invoice.Models;

namespace ePlatform.Api.eBelge.Invoice
{
    public class InboxInvoiceClient
    {
        private readonly IFlurlClient flurlClient;

        public InboxInvoiceClient(ClientOptions clientOptions, IFlurlClientFactory flurlClientFac)
        {
            flurlClient = flurlClientFac.Get(clientOptions.InvoiceServiceUrl).SetDefaultSettings();
            flurlClient.Headers["Client-Version"] = Assembly.GetExecutingAssembly().GetName().Version;
        }

        public async Task<InboxInvoiceGetModel> Get(Guid id)
        {
            return await flurlClient.Request($"/v1/inboxinvoice/{id}")
                .GetJsonAsync<InboxInvoiceGetModel>();
        }

        public async Task<UblBuilderModel> GetInvoice(Guid id)
        {
            return await flurlClient.Request($"/v1/inboxinvoice/getinvoice")
                .SetQueryParam("guid", id)
                .GetJsonAsync<UblBuilderModel>();
        }

        public async Task<PagedList<InboxInvoiceGetModel>> Get(PagingModel model)
        {
            return await flurlClient.Request($"/v1/inboxinvoice/list")
                .SetQueryParams(model)
                .GetJsonAsync<PagedList<InboxInvoiceGetModel>>();
        }

        public async Task<Stream> GetHtml(Guid id, bool useStandartXslt = false)
        {
            return await flurlClient.Request($"/v2/inboxinvoice/{id}/html/{useStandartXslt}")
                .GetStreamAsync();
        }

        public async Task<Stream> GetPdf(Guid id, bool useStandartXslt)
        {
            return await flurlClient.Request($"/v2/inboxinvoice/{id}/pdf/{useStandartXslt}")
                .GetStreamAsync();
        }

        /// <summary>
        /// Gets a ZIP of the inbox invoice with the given id value as a <see cref="Stream"/>.
        /// </summary>
        /// <param name="id">The id of the inbox invoice</param>
        /// <param name="useStandardXslt">If this field is sent as false, then the invoice is returned according to the
        /// default one among the XSLTs you have added. Otherwise, the standard XSLT ticket is returned.</param>
        /// <returns>The task result contains the <see cref="Stream"/> object.</returns>
        public async Task<Stream> GetZip(Guid id, bool useStandardXslt = false)
        {
            return await flurlClient.Request($"/v2/inboxinvoice/{id}/zip/{useStandardXslt}")
                .GetStreamAsync();
        }

        public async Task<byte[]> GetPdfList(InvoicePdfModel model)
        {
            var reponse = await flurlClient.Request($"/v1/inboxinvoice/pdflist")
                .PostJsonAsync(model);
            return await reponse.Content.ReadAsByteArrayAsync();
        }

        public async Task<Stream> GetUbl(Guid id)
        {
            return await flurlClient.Request($"/v2/inboxinvoice/{id}/ubl")
                .GetStreamAsync();
        }

        public async Task<ApproveRejectInvoiceModel> GetInvoiceResponse(Guid id)
        {
            return await flurlClient.Request($"/v1/invoiceresponse/getbyinvoiceid/{id}")
                .GetJsonAsync<ApproveRejectInvoiceModel>();
        }

        public async Task<DocumentResponseModel> ApproveReject(ApproveRejectInvoiceModel model)
        {
            var reponseModel = await flurlClient.Request($"/v1/invoiceresponse")
                .PostJsonAsync(model)
                .ReceiveJson<DocumentResponseModel>();
            return reponseModel;
        }

        /// <summary>
        /// Retry invoices that are in <see cref="InvoiceStatus.FailedApprove"/> or <see cref="InvoiceStatus.FailedDecline"/>
        /// or <see cref="InvoiceStatus.FailedReturn"/> status.
        /// </summary>
        /// <param name="ids">Model for the selected invoice ids.</param>
        /// <returns>The task result contains the info about if the retry operation succeeds or not.</returns>
        public async Task<bool> RetryInvoices(List<Guid> ids)
        {
            var response = await flurlClient.Request($"/v1/invoiceresponse/retryinvoiceresponselist")
                .PutJsonAsync(ids);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Inbox invoices are saved to the system as <see cref="UpdateIsNewModel.IsNew"/> true. 
        /// If you do not want to take again the invoices you have taken to your accounting / ERP program, 
        /// you can update <see cref="UpdateIsNewModel.IsNew"/> to false.
        /// </summary>
        /// <param name="models">Models for the selected inbox invoice ids and their <see cref="UpdateIsNewModel.IsNew"/> status.</param>
        /// <returns>The task result contains the numbers of updated invoices.</returns>
        public async Task<int> UpdateIsNew(List<UpdateIsNewModel> models)
        {
            return await flurlClient.Request($"/v1/inboxinvoice/updateisnew")
                .PutJsonAsync(models)
                .ReceiveJson<int>();
        }
    }
}
