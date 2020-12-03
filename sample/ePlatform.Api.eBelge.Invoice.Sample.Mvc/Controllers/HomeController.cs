using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Invoice.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ePlatform.Api.eBelge.Invoice.Sample.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly CommonClient commonClient;
        private readonly OutboxInvoiceClient outboxInvoiceClient;

        public HomeController(CommonClient commonClient, OutboxInvoiceClient outboxInvoiceClient)
        {
            this.commonClient = commonClient;
            this.outboxInvoiceClient = outboxInvoiceClient;
        }
        public async Task<ActionResult> Index()
        {
            return View(await commonClient.CountrList());
        }

        public async Task<ActionResult> Invoices()
        {
            var model = new QueryFilterBuilder<OutboxInvoiceGetModel>()
                .PageIndex(1)
                .QueryFor(q => q.ExecutionDate, Operator.LessThan, DateTime.Now)
                .QueryFor(q => q.Currency, Operator.Equal, "TRY")
                // .QueryFor(q => q.InvoiceNumber, Operator.Equal, "EPK2019000001731")
                // .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Error)
                .Build();
            var invoices = await outboxInvoiceClient.GetList(model);
            return View(invoices.Items);
        }
    }
}
