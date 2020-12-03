using ePlatform.Api.Core.Http;
using ePlatform.Api.eBelge.Invoice.Models;
using Flurl.Http;
using Flurl.Http.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace ePlatform.Api.eBelge.Invoice
{
    public class EArchiveInvoiceClient
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
        public EArchiveInvoiceClient(ClientOptions clientOptions, IFlurlClientFactory flurlClientFac)
        {
            flurlClient = flurlClientFac.Get(clientOptions.InvoiceServiceUrl).SetDefaultSettings();
            flurlClient.Headers["Client-Version"] = Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// Get One Outbox Invoice with Guid Id
        /// </summary>
        public async Task<OutboxInvoiceGetModel> Get(Guid id)
        {
            return await flurlClient.Request($"/v1/earchive/{id}")
                .GetJsonAsync<OutboxInvoiceGetModel>();
        }

        /// <summary>
        /// Email Status:
        /// Created=0,Queued=10,Send=20,Failed=30,SendStopped=40
        /// </summary>
        public async Task<List<EarsivInvoiceMailModel>> GetMailDetail(string id)
        {
            return await flurlClient.Request($"/v1/earchive/getmaildetail")
                .SetQueryParam("id", id.ToString())
                .GetJsonAsync<List<EarsivInvoiceMailModel>>();
        }

        public async Task<bool> Cancel(Guid[] ids)
        {
            var response = await flurlClient.Request($"/v1/earchive/cancelinvoice")
                .PutJsonAsync(ids);
            return response.IsSuccessStatusCode;
        }

        public async Task RetryInvoiceMail(Guid id)
        {
            await flurlClient.Request($"/v1/earchive/retryinvoicemail/{id}")
                .GetAsync();
        }

        public async Task<bool> RetryInvoiceWithDifferentMail(Guid id, string mail)
        {
            var response = await flurlClient.Request($"/v1/earchive/retryinvoicemail/{id}/{mail}")
                .PostAsync(null);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retries the e-mail sent for invoice or to send it to a new e-mail address.
        /// </summary>
        /// <param name="retryMailModel">Model for the selected invoice id and email addresses</param>
        /// <returns>The task result contains the info about if the retry operation succeeds or not.</returns>
        public async Task<bool> RetryInvoiceWithDifferentMails(RetryMailModel retryMailModel)
        {
            var response = await flurlClient.Request($"/v1/earchive/retryinvoicemail")
                .SetQueryParams(retryMailModel)
                .GetAsync();
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Updates the status of your wrong e-Archive invoices as draft The draft invoice can be arranged and sent again.
        /// </summary>
        /// <param name="ids">The ids of e-Archive invoices</param>
        /// <returns>The task result contains the info about if the back to draft operation succeeds or not.</returns>
        public async Task<bool> BackToDraft(List<Guid> ids)
        {
            var response = await flurlClient.Request($"/v1/earchive/backtodraft")
                .SetQueryParams(new {ids})
                .GetAsync();
            return response.IsSuccessStatusCode;
        }
    }
}
