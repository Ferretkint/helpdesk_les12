using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Model;
using VivesHelpdesk.Services;

namespace VivesHelpdesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        //Find: /api/tickets
        [HttpGet("{assignedToId:int?}")]
        public IActionResult Find(int? assignedToId)
        {
            var tickets = _ticketService.Find();
            return Ok(tickets);
        }

        //Get: /api/tickets/1
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute]int id)
        {
            var tickets = _ticketService.Get(id);
            return Ok(tickets);
        }

        //Create
        [HttpPost]
        public IActionResult Create([FromBody]Ticket ticket)
        {
            var createdTicket = _ticketService.Create(ticket);
            return Ok(createdTicket);
        }

        //Edit
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody] Ticket ticket)
        {
            var createdTicket = _ticketService.Update(id, ticket);
            return Ok(createdTicket);
        }

        //Delete
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _ticketService.Delete(id);
            return Ok();
        }
    }
}
