using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ePlatform.Api.Core;
using ePlatform.Api.eBelge.Ticket.Common.Enums;
using ePlatform.Api.eBelge.Ticket.EventTicket;
using ePlatform.Api.eBelge.Ticket.EventTicket.Models;
using ePlatform.Api.eBelge.Ticket.PassengerTicket;
using ePlatform.Api.eBelge.Ticket.PassengerTicket.Models;

namespace ePlatform.Api.eBelge.Ticket.Sample.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventTicketClient _eventTicketClient;
        private readonly PassengerTicketClient _passengerTicketClient;

        public HomeController(EventTicketClient eventTicketClient,PassengerTicketClient passengerTicketClient)
        {
            _eventTicketClient = eventTicketClient;
            _passengerTicketClient = passengerTicketClient;
        }

        public async Task<ActionResult> EventTickets()
        {
            var approvedEventTicketList = await _eventTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .PageSize(10)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());
            
            return View(approvedEventTicketList.Items);
        }
        
        public async Task<ActionResult> PassengerTickets()
        {
            var approvedPassengerTicketList = await _passengerTicketClient.GetTicketList(
                new QueryFilterBuilder<PassengerTicketModel>()
                    .PageSize(10)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());
            
            return View(approvedPassengerTicketList.Items);
        }
    }
}
