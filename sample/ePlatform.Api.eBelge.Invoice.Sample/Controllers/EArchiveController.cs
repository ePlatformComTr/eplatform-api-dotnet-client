using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Invoice.Models;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.eBelge.Invoice.Sample.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EArchiveController : ControllerBase
    {
        private readonly EArchiveInvoiceClient earchiveClient;

        public EArchiveController(EArchiveInvoiceClient earchiveClient)
        {
            this.earchiveClient = earchiveClient;
        }

        [HttpGet("{id}")] //B980FFE7-A6F0-4072-AACE-119CCB40A483
        public async Task<ActionResult<OutboxInvoiceGetModel>> Get(Guid id)
        {
            return await earchiveClient.Get(id);
        }

        [HttpGet("cancelinvoice")] //f201ba2e-881f-4798-a715-d6090a28d7b2
        public async Task<ActionResult> Get()
        {
            Guid[] model = new Guid[]{
                new Guid("f201ba2e-881f-4798-a715-d6090a28d7b2"),
                new Guid("f201ba2e-881f-4798-b623-d6090a28d7b2")};
            return Ok(await earchiveClient.Cancel(model));
        }

        // [HttpGet("list")]
        // public async Task<ActionResult<PagedList<OutboxInvoiceGetModel>>> GetList()
        // {
        //     var model = new QueryFilterBuilder<OutboxInvoiceGetModel>()
        //         .PageIndex(1)
        //         .QueryFor(q => q.ExecutionDate, Operator.LessThan, DateTime.Now)
        //         // .QueryFor(q => q.InvoiceNumber, Operator.Equal, "EPA2017000000009")
        //         // .QueryFor(q => q.Status, Operator.Equal, InvoiceStatus.Error)
        //         // .QueryFor(x => x.Id, Operator.Equal, "26284d90-2a2c-4b50-908a-5122da62646d")
        //         .Build();
        //     return await earchiveClient.Get(model);
        // }

        [HttpGet("getmailddetail/{id}")] //e6f321ba-12c3-460b-87b3-04ac9887deb
        public async Task<ActionResult<List<EarsivInvoiceMailModel>>> GetMailDetail(string id)
        {
            return Ok(await earchiveClient.GetMailDetail(id));
        }


    }
}
