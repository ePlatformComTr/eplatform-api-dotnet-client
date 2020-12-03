using System;
using System.Threading.Tasks;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.PassengerTicket;
using ePlatform.Api.eBelge.Ticket.PassengerTicket.Models;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.eBelge.Ticket.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerTicketController : ControllerBase
    {
        private readonly PassengerTicketClient _passengerTicketClient;

        public PassengerTicketController(PassengerTicketClient passengerTicketClient)
        {
            _passengerTicketClient = passengerTicketClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerTicketModel>> Get(Guid id)
        {
            return await _passengerTicketClient.Get(id);
        }
        
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<TicketBuilderModel>> GetDetail(Guid id)
        {
            return await _passengerTicketClient.GetDetail(id);
        }
    }
}
