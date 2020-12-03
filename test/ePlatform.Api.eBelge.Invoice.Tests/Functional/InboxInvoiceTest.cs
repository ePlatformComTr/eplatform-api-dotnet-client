using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Invoice.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace ePlatform.Api.eBelge.Invoice.Tests.Functional
{
    [Collection("eBelge")]
    public class InboxInvoiceTest
    {
        private readonly InboxInvoiceClient _inboxInvoiceClient;
        private readonly InboxInvoiceGetModel _model;
        public InboxInvoiceTest(StartupFixture fixture)
        {
            _inboxInvoiceClient = fixture.ServiceProvider.GetRequiredService<InboxInvoiceClient>();

            var query = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(3)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                .Build();
            var pageList = _inboxInvoiceClient.Get(query).Result;
            _model = pageList.Items.First();
        }

        [Fact]
        public async Task Should_Get_InboxInvoice_Summary()
        {
            var response = await _inboxInvoiceClient.Get(_model.Id);

            var updatedModel = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_model));
            updatedModel.Property("Envelope").Remove();
            var updatedResponse = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(response));
            updatedResponse.Property("Envelope").Remove();

            Assert.Equal(JsonConvert.SerializeObject(updatedModel), JsonConvert.SerializeObject(updatedResponse));
        }

        [Fact]
        public async Task Should_Get_InboxInvoice()
        {
            var response = await _inboxInvoiceClient.GetInvoice(_model.Id);

            Assert.Equal(response.InvoiceId, _model.Id);
            Assert.NotNull(response.AddressBook);
            Assert.NotNull(response.GeneralInfoModel);
        }

        [Fact]
        public async Task Should_Get_Paged_InboxInvoice()
        {
            var query = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(3)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Approved)
                .Build();

            var response = await _inboxInvoiceClient.Get(query);

            Assert.NotNull(response);
            Assert.True(response.Items.All(q => q.Currency == "TRY"));
            Assert.True(response.Items.All(q => q.Status == (int)InvoiceStatus.Approved));
        }

        [Fact]
        public async Task Should_Get_InboxInvoice_As_Pdf_Stream()
        {
            var streamData = await _inboxInvoiceClient.GetPdf(_model.Id, false);

            using var reader = new StreamReader(streamData, Encoding.UTF8);
            var value = await reader.ReadToEndAsync();
            Assert.Contains("PDF", value);
        }

        [Fact]
        public async Task Should_Get_InboxInvoice_As_Html_Stream()
        {
            var streamData = await _inboxInvoiceClient.GetHtml(_model.Id);

            using var reader = new StreamReader(streamData, Encoding.UTF8);
            var value = await reader.ReadToEndAsync();
            Assert.Contains("<html>", value);
            Assert.Contains("</html>", value);
            Assert.Contains("<head>", value);
        }

        [Fact]
        public async Task Should_Get_InboxInvoice_As_Ubl_Stream()
        {
            var streamData = await _inboxInvoiceClient.GetUbl(_model.Id);

            using var reader = new StreamReader(streamData, Encoding.UTF8);
            var value = await reader.ReadToEndAsync();
            Assert.NotEmpty(value);
            Assert.Contains("<Invoice", value);
            Assert.Contains("<cbc:ID schemeID=", value);
            Assert.Contains("<cac:PartyIdentification>", value);
            Assert.Contains("<cac:PartyIdentification>", value);
        }

        [Fact]
        public async Task Should_Get_InboxInvoice_Zip_As_Stream()
        {
            var streamData = await _inboxInvoiceClient.GetZip(_model.Id);

            Assert.NotNull(streamData);
        }


        [Fact]
        public async Task Should_Get_InboxInvoice_PdfList_As_Byte_Array()
        {
            var pdfListByteArray = await _inboxInvoiceClient.GetPdfList(new InvoicePdfModel
            {
                IsStandartXslt = false,
                Ids = new[] {_model.Id}
            });

            Assert.NotNull(pdfListByteArray);
        }

        [Fact]
        public async Task Should_Update_Invoice_isNew()
        {
         var numberOfUpdatedItems=   await _inboxInvoiceClient.UpdateIsNew(new List<UpdateIsNewModel>
            {
                new UpdateIsNewModel
                {
                    InvoiceId = _model.Id,
                    IsNew = false
                }
            });
         
         Assert.Equal(1,numberOfUpdatedItems);
         
        }

        [Fact]
        public async Task Should_Throw_Http_404_EntityNotFoundException()
        {
            var notExistingInvoiceId = Guid.Parse("85733EDC-958B-4C80-9E49-8942B85D0156");
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _inboxInvoiceClient.Get(notExistingInvoiceId);
            });
        }

        [Fact]
        public async Task Should_Page_Size_Equals_Returned_Number_Of_Items()
        {
            var query = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(3)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                .Build();

            var result = await _inboxInvoiceClient.Get(query);
            Assert.True(result.Items.All(q => q.Currency == "TRY"));
            Assert.Equal(3, result.Items.Count());
        }
    }
}
