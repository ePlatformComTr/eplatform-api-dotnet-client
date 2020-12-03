using System;
using System.Linq;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Invoice.Models;
using ePlatform.Api.eBelge.Invoice.Tests.Builders;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ePlatform.Api.eBelge.Invoice.Tests.Functional
{
    [Collection("eBelge")]
    public class OutboxInvoiceTest
    {
        private readonly UblBuilderModelBuilder _ublBuilderModelBuilder;
        private readonly OutboxInvoiceClient _outboxInvoiceClient;

        public OutboxInvoiceTest(StartupFixture fixture)
        {
            _ublBuilderModelBuilder = fixture.ServiceProvider.GetRequiredService<UblBuilderModelBuilder>();
            _outboxInvoiceClient = fixture.ServiceProvider.GetRequiredService<OutboxInvoiceClient>();
        }

        [Fact]
        public async Task Should_Create_OutboxInvoice()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            Assert.True(createInvoiceResponseModel.InvoiceNumber.Trim().Length > 0);
            Assert.True(createInvoiceResponseModel.Id.Trim().Length > 0);
        }

        [Fact]
        public async Task Should_Create_OutboxInvoice_With_Manual_Invoice_Id()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues()
                .With(x => x.UseManualInvoiceId = true)
                .With(x => x.GeneralInfoModel.InvoiceNumber =
                    $"ABC{DateTime.Now.Year}{new Random().Next(100000000, 999999999)}")
                .Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            Assert.Equal(ublBuilderModel.GeneralInfoModel.Ettn, new Guid(createInvoiceResponseModel.Id));
            Assert.Equal(ublBuilderModel.GeneralInfoModel.InvoiceNumber, ublBuilderModel.GeneralInfoModel.InvoiceNumber);
        }

        [Fact]
        public async Task Should_Get_OutboxInvoice_Summary()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var outboxInvoiceGetModel = await _outboxInvoiceClient.Get(new Guid(createInvoiceResponseModel.Id));

            Assert.Equal(ublBuilderModel.GeneralInfoModel.Ettn, outboxInvoiceGetModel.Id);
            Assert.NotNull(outboxInvoiceGetModel.InvoiceNumber);
            if (outboxInvoiceGetModel.ExecutionDate != null)
                Assert.Equal(
                    ublBuilderModel.GeneralInfoModel.IssueDate.AddTicks(-ublBuilderModel.GeneralInfoModel.IssueDate.Ticks % TimeSpan.TicksPerSecond),
                    outboxInvoiceGetModel.ExecutionDate.Value.AddTicks(-outboxInvoiceGetModel.ExecutionDate.Value.Ticks % TimeSpan.TicksPerSecond));
            if (ublBuilderModel.GeneralInfoModel.InvoiceProfileType != null)
                Assert.Equal((int)ublBuilderModel.GeneralInfoModel.InvoiceProfileType, outboxInvoiceGetModel.Type);
            Assert.Equal(ublBuilderModel.Status, outboxInvoiceGetModel.Status);
            Assert.Equal(ublBuilderModel.AddressBook.Name, outboxInvoiceGetModel.TargetTitle);
            Assert.Equal(ublBuilderModel.AddressBook.IdentificationNumber, outboxInvoiceGetModel.TargetVknTckn);
            Assert.Equal(ublBuilderModel.AddressBook.Alias, outboxInvoiceGetModel.TargetAlias);
            Assert.False(outboxInvoiceGetModel.IsArchived);
            Assert.Equal(ublBuilderModel.InvoiceLines.Sum(x => x.UnitPrice),
                outboxInvoiceGetModel.TotalAmount);
            Assert.Equal(ublBuilderModel.InvoiceLines.Sum(x => x.UnitPrice - x.DiscountAmount) +
                         ublBuilderModel.InvoiceLines.Sum(x => x.VatAmount) +
                         ublBuilderModel.InvoiceLines.SelectMany(x => x.Taxes)
                             .Sum(x => x.TaxAmount)
                , outboxInvoiceGetModel.PayableAmount);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.CurrencyCode, outboxInvoiceGetModel.Currency);
            Assert.Equal(ublBuilderModel.InvoiceLines.Sum(x => x.VatAmount) +
                         ublBuilderModel.InvoiceLines.SelectMany(x => x.Taxes)
                             .Sum(x => x.TaxAmount),
                outboxInvoiceGetModel.TaxTotal);
            Assert.Null(outboxInvoiceGetModel.ResponseEnvelopeId);
            Assert.Null(outboxInvoiceGetModel.LocalReferenceId);
            Assert.Null(outboxInvoiceGetModel.Message);
            Assert.Equal(ublBuilderModel.RecordType, outboxInvoiceGetModel.AppType);
            Assert.Null(outboxInvoiceGetModel.Reason);
            Assert.Null(outboxInvoiceGetModel.Prefix);
            Assert.Null(outboxInvoiceGetModel.DigestValue);
            Assert.Null(outboxInvoiceGetModel.EarsivInvoice);
        }

        [Fact]
        public async Task Should_Get_OutboxInvoice()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var outboxInvoice = await _outboxInvoiceClient.GetInvoice(new Guid(createInvoiceResponseModel.Id));

            Assert.NotEqual(Guid.Empty, outboxInvoice.InvoiceId);
            Assert.Equal(0, outboxInvoice.Status); //Draft
            Assert.False(outboxInvoice.IsNew);
            Assert.Equal("master", outboxInvoice.XsltCode);
            Assert.Null(outboxInvoice.LocalReferenceId);
            Assert.False(outboxInvoice.UseManualInvoiceId);
            Assert.Null(outboxInvoice.EArsivInfo);
            Assert.False(outboxInvoice.isSend);
            Assert.Equal(ublBuilderModel.RecordType, outboxInvoice.RecordType);
            Assert.Empty(outboxInvoice.Notes);
            Assert.Empty(outboxInvoice.RelatedDespatchList);
            Assert.Empty(outboxInvoice.CustomDocumentReferenceList);
            Assert.Empty(outboxInvoice.AllowanceCharges);

            //Addressbook
            Assert.Equal(ublBuilderModel.AddressBook.IsArchive, outboxInvoice.AddressBook.IsArchive);
            Assert.Equal(ublBuilderModel.AddressBook.Alias, outboxInvoice.AddressBook.Alias);
            Assert.Equal(ublBuilderModel.AddressBook.IdentificationNumber, outboxInvoice.AddressBook.IdentificationNumber);
            Assert.Equal(ublBuilderModel.AddressBook.Name, outboxInvoice.AddressBook.Name);
            Assert.Equal(ublBuilderModel.AddressBook.RegisterNumber, outboxInvoice.AddressBook.RegisterNumber);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverStreet, outboxInvoice.AddressBook.ReceiverStreet);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverBuildingName, outboxInvoice.AddressBook.ReceiverBuildingName);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverBuildingNumber, outboxInvoice.AddressBook.ReceiverBuildingNumber);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverDoorNumber, outboxInvoice.AddressBook.ReceiverDoorNumber);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverSmallTown, outboxInvoice.AddressBook.ReceiverSmallTown);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverDistrict, outboxInvoice.AddressBook.ReceiverDistrict);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverZipCode, outboxInvoice.AddressBook.ReceiverZipCode);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverCity, outboxInvoice.AddressBook.ReceiverCity);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverCountry, outboxInvoice.AddressBook.ReceiverCountry);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverPhoneNumber, outboxInvoice.AddressBook.ReceiverPhoneNumber);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverFaxNumber, outboxInvoice.AddressBook.ReceiverFaxNumber);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverEmail, outboxInvoice.AddressBook.ReceiverEmail);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverWebSite, outboxInvoice.AddressBook.ReceiverWebSite);
            Assert.Equal(ublBuilderModel.AddressBook.ReceiverTaxOffice, outboxInvoice.AddressBook.ReceiverTaxOffice);
            Assert.Equal(ublBuilderModel.AddressBook.IsDeleted, outboxInvoice.AddressBook.IsDeleted);
            Assert.Equal(ublBuilderModel.AddressBook.Type, outboxInvoice.AddressBook.Type);
            Assert.Equal(ublBuilderModel.AddressBook.Status, outboxInvoice.AddressBook.Status);
            Assert.Equal(ublBuilderModel.AddressBook.UpdatedDate, outboxInvoice.AddressBook.UpdatedDate);
            Assert.Equal(ublBuilderModel.AddressBook.IsSaveAddress, outboxInvoice.AddressBook.IsSaveAddress);

            //GeneralInfoModel
            Assert.Equal(ublBuilderModel.GeneralInfoModel.Ettn, outboxInvoice.GeneralInfoModel.Ettn);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.Prefix, outboxInvoice.GeneralInfoModel.Prefix);
            Assert.NotNull(outboxInvoice.GeneralInfoModel.InvoiceNumber);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.SlipNumber, outboxInvoice.GeneralInfoModel.SlipNumber);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.InvoiceProfileType, outboxInvoice.GeneralInfoModel.InvoiceProfileType);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.IssueDate, outboxInvoice.GeneralInfoModel.IssueDate);
            Assert.NotNull(outboxInvoice.GeneralInfoModel.IssueTime);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.DeliveryDate, outboxInvoice.GeneralInfoModel.DeliveryDate);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.Type, outboxInvoice.GeneralInfoModel.Type);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.ReturnInvoiceNumber, outboxInvoice.GeneralInfoModel.ReturnInvoiceNumber);
            Assert.NotNull(outboxInvoice.GeneralInfoModel.ReturnInvoiceDate);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.CurrencyCode, outboxInvoice.GeneralInfoModel.CurrencyCode);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.ExchangeRate, outboxInvoice.GeneralInfoModel.ExchangeRate);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.DespatchNumber, outboxInvoice.GeneralInfoModel.DespatchNumber);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.DespatchType, outboxInvoice.GeneralInfoModel.DespatchType);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.DespatchProfileType, outboxInvoice.GeneralInfoModel.DespatchProfileType);
            Assert.Equal(ublBuilderModel.GeneralInfoModel.TotalAmount, outboxInvoice.GeneralInfoModel.TotalAmount);

            //InvoiceLines
            Assert.Equal(ublBuilderModel.InvoiceLines[0].InventoryServiceType, outboxInvoice.InvoiceLines[0].InventoryServiceType);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].InventoryCard, outboxInvoice.InvoiceLines[0].InventoryCard);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].SerialNoEnabled, outboxInvoice.InvoiceLines[0].SerialNoEnabled);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].Amount, outboxInvoice.InvoiceLines[0].Amount);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].UnitCode, outboxInvoice.InvoiceLines[0].UnitCode);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].UnitPrice, outboxInvoice.InvoiceLines[0].UnitPrice);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].DiscountRate, outboxInvoice.InvoiceLines[0].DiscountRate);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].DiscountAmount, outboxInvoice.InvoiceLines[0].DiscountAmount);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].LineExtensionAmount, outboxInvoice.InvoiceLines[0].LineExtensionAmount);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].VatRate, outboxInvoice.InvoiceLines[0].VatRate);
            Assert.Equal(ublBuilderModel.InvoiceLines[0].VatAmount, outboxInvoice.InvoiceLines[0].VatAmount);
        }

        [Fact]
        public async Task Should_Get_OutboxInvoice_List()
        {
            var pagedOutboxInvoices = await _outboxInvoiceClient.GetList(new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(3)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                .Build());

            Assert.NotNull(pagedOutboxInvoices);
            Assert.NotNull(pagedOutboxInvoices.Items);
            Assert.NotEmpty(pagedOutboxInvoices.Items);
            Assert.Equal(3, pagedOutboxInvoices.PageSize);
            foreach (var outboxInvoiceGetModel in pagedOutboxInvoices.Items)
                Assert.Equal("TRY", outboxInvoiceGetModel.Currency);
        }

        [Fact]
        public async Task Should_Update_OutboxInvoice()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var outboxInvoice = await _outboxInvoiceClient.GetInvoice(new Guid(createInvoiceResponseModel.Id));

            outboxInvoice.AddressBook.Name = "Updated Receiver Name";
            await _outboxInvoiceClient.Update(outboxInvoice.InvoiceId, outboxInvoice);

            var updatedOutboxInvoice = await _outboxInvoiceClient.GetInvoice(new Guid(createInvoiceResponseModel.Id));

            Assert.Equal("Updated Receiver Name", updatedOutboxInvoice.AddressBook.Name);
        }

        [Fact]
        public async Task Should_Update_Statuses_Of_Outbox_Invoices()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var ids = new Guid[1];
            ids.SetValue(new Guid(createInvoiceResponseModel.Id), 0);

            var isSucceed = await _outboxInvoiceClient.UpdateStatusList(new UpdateInvoiceModel
            {
                Ids = ids,
                Status = InvoiceStatus.Queued
            });

            var pagedOutboxInvoices = await _outboxInvoiceClient.GetList(new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(3)
                .QueryFor(q => q.Id, Operator.Equal, createInvoiceResponseModel.Id)
                .Build());

            Assert.True(isSucceed);
            foreach (var outboxInvoiceGetModel in pagedOutboxInvoices.Items)
            {
                Assert.Equal(20, outboxInvoiceGetModel.Status);
            }
        }

        [Fact]
        public async Task Should_Get_Outbox_Invoice_As_Ubl_Stream()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var ublStream = await _outboxInvoiceClient.GetUbl(new Guid(createInvoiceResponseModel.Id));

            Assert.NotNull(ublStream);
        }

        [Fact]
        public async Task Should_Get_Outbox_Invoice_As_Pdf_Stream()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var pdfStream = await _outboxInvoiceClient.GetPdf(new Guid(createInvoiceResponseModel.Id));

            Assert.NotNull(pdfStream);
        }

        [Fact]
        public async Task Should_Get_Outbox_Invoice_As_Zip_Stream()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var zipStream = await _outboxInvoiceClient.GetZip(new Guid(createInvoiceResponseModel.Id));

            Assert.NotNull(zipStream);
        }

        [Fact]
        public async Task Should_Get_Outbox_Invoice_As_Html_Stream()
        {
            var ublBuilderModel = _ublBuilderModelBuilder.CreateWithDefaultValues().Build();

            var createInvoiceResponseModel = await _outboxInvoiceClient.Post(ublBuilderModel);

            var htmlStream = await _outboxInvoiceClient.GetHtml(new Guid(createInvoiceResponseModel.Id));

            Assert.NotNull(htmlStream);
        }

        [Fact]
        public async Task Should_Get_Invoice_Envelope()
        {
            var pagedOutboxInvoices = await _outboxInvoiceClient.GetList(new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(1)
                .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Approved)
                .QueryFor(q => q.EnvelopeId, Operator.NotEqual, null)
                .Build());


            var approveRejectInvoiceModel = await _outboxInvoiceClient.GetInvoiceEnvelope(pagedOutboxInvoices.Items.First().EnvelopeId.Value, true);

            Assert.NotNull(approveRejectInvoiceModel);
            Assert.Equal(pagedOutboxInvoices.Items.First().EnvelopeId.Value, approveRejectInvoiceModel.Id);
        }

        [Fact]
        public async Task Should_Throw_Http_404_EntityNotFoundException()
        {
            var notExistingInvoiceId = Guid.Parse("85733EDC-958B-4C80-9E49-8942B85D0156");
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _outboxInvoiceClient.Get(notExistingInvoiceId);
            });
        }

        [Fact]
        public async Task Should_Throw_Http_422_EntityValidationException()
        {
            var ublModel = new UblBuilderModel
            {
                GeneralInfoModel = new GeneralInfoBaseModel()
                {
                    Ettn = Guid.NewGuid(),
                    Prefix = null,
                    InvoiceNumber = null,
                    InvoiceProfileType = InvoiceProfileType.TEMELFATURA,
                    IssueDate = DateTime.Now,
                    Type = InvoiceType.SATIS,
                    CurrencyCode = "TRY"
                },
                AddressBook = new AddressBookModel()
                {
                    Alias = "urn:mail:defaulttest11pk@medyasoft.com.tr",
                    IdentificationNumber = "1234567801111111111", //invalid vkn
                }
            };
            await Assert.ThrowsAsync<EntityValidationException>(async () =>
            {
                await _outboxInvoiceClient.Post(ublModel);
            });
        }

        [Fact]
        public async Task Should_Page_Size_Equals_Returned_Number_Of_Items()
        {
            var query = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageSize(3)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                .Build();

            var result = await _outboxInvoiceClient.GetList(query);
            Assert.Equal(3, result.PageSize);
            Assert.Equal(3, result.Items.Count());
        }
    }
}
