using System;
using System.Linq;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Invoice.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace ePlatform.Api.eBelge.Invoice.Tests.Functional
{
    [Collection("eBelge")]
    public class EArchiveInvoiceTest
    {
        private readonly EArchiveInvoiceClient _eArchiveInvoiceClient;
        private readonly OutboxInvoiceClient _outboxInvoiceClient;
        private readonly OutboxInvoiceGetModel _model;

        public EArchiveInvoiceTest(StartupFixture fixture)
        {
            _eArchiveInvoiceClient = fixture.ServiceProvider.GetRequiredService<EArchiveInvoiceClient>();
            _outboxInvoiceClient = fixture.ServiceProvider.GetRequiredService<OutboxInvoiceClient>();
            var query = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(3)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Approved)
                .QueryFor(q => q.Id, Operator.Contains, "d75d7747-7912-4df6-913a-7ffb8cdb7f3e")
                .Build();
            var pageList = _outboxInvoiceClient.GetList(query).Result;
            _model = pageList.Items.First();
        }


        [Fact]
        public async Task Should_Get_EArchive_Invoice()
        {
            var eArchiveInvoice = await _eArchiveInvoiceClient.Get(_model.Id);

            Assert.NotNull(eArchiveInvoice);
            Assert.Equal(JsonConvert.SerializeObject(_model), JsonConvert.SerializeObject(eArchiveInvoice));
        }

        [Fact]
        public async Task Should_Get_Mail_Details()
        {
            var eArchiveInvoiceMailModels = await _eArchiveInvoiceClient.GetMailDetail(_model.Id.ToString());

            Assert.NotNull(eArchiveInvoiceMailModels);
            Assert.NotEmpty(eArchiveInvoiceMailModels);

            Assert.Equal(_model.Id, eArchiveInvoiceMailModels.First().InvoiceId);
        }

        [Fact]
        public async Task Should_Cancel_EArchive_Invoices()
        {
            var query = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(2)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                .QueryFor(q => q.ExecutionDate, Operator.GreaterThan, DateTime.Now.AddMonths(-5))
                .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Approved)
                .Build();
            var pagedEArchiveInvoices = _outboxInvoiceClient.GetList(query).Result;

            var isSucceed = await _eArchiveInvoiceClient.Cancel(pagedEArchiveInvoices.Items.Select(x => x.Id).ToArray());
            Assert.True(isSucceed);
        }

        [Fact]
        public async Task Should_Retry_EArchive_Invoice_Mail_With_Different_Mail()
        {
            var isSucceed = await _eArchiveInvoiceClient.RetryInvoiceWithDifferentMail(_model.Id, "serviceuser01@isim360.com");
            Assert.True(isSucceed);
        }

        [Fact]
        public async Task Should_Retry_EArchive_Invoice_Mail_With_Different_Mails()
        {
            var isSucceed = await _eArchiveInvoiceClient.RetryInvoiceWithDifferentMails(new RetryMailModel
            {
                Id = _model.Id,
                EmailAddresses = "serviceuser01@isim360.com;burhansavci@service.com"
            });

            Assert.True(isSucceed);
        }

        [Fact]
        public async Task Should_Back_EArchive_Invoices_To_Draft()
        {
            var query = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(2)
                .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Error)
                .Build();
            var pagedEArchiveInvoices = _outboxInvoiceClient.GetList(query).Result;
            var isSucceed = await _eArchiveInvoiceClient.BackToDraft(pagedEArchiveInvoices.Items.Select(x => x.Id).ToList());
            var queryForDraftEarchiveInvoices = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(2)
                .QueryFor(q => q.Id, Operator.Equal, pagedEArchiveInvoices.Items.Select(x => x.Id).First())
                .Build();
            var pagedDraftEArchiveInvoices = _outboxInvoiceClient.GetList(queryForDraftEarchiveInvoices).Result;

            Assert.True(isSucceed);
            foreach (var draftEarchiveInvoice in pagedDraftEArchiveInvoices.Items)
                Assert.Equal((int)InvoiceStatus.Draft, draftEarchiveInvoice.Status);
        }

        [Fact]
        public async Task Should_Throw_Http_404_EntityNotFoundException_On_Cancel_Method()
        {
            var notExistingInvoiceId = Guid.Parse("85733EDC-958B-4C80-9E49-8942B85D0156");
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _eArchiveInvoiceClient.Cancel(new Guid[] {notExistingInvoiceId});
            });
        }
    }
}
