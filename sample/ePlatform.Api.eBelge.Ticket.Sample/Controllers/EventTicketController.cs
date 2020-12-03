using System;
using System.Threading.Tasks;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.EventTicket;
using ePlatform.Api.eBelge.Ticket.EventTicket.Models;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.eBelge.Ticket.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTicketController : ControllerBase
    {
        private readonly EventTicketClient _eventTicketClient;

        public EventTicketController(EventTicketClient eventTicketClient)
        {
            _eventTicketClient = eventTicketClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventTicketModel>> Get(Guid id)
        {
            return await _eventTicketClient.Get(id);
        }
        
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<TicketBuilderModel>> GetDetail(Guid id)
        {
            return await _eventTicketClient.GetDetail(id);
        }
    }
}
