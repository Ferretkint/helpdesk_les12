using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Model;
using VivesHelpdesk.Ui.WebApp.Sdk;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketSdk _ticketSdk;
        private readonly PersonSdk _personSdk;

        public TicketController(
            PersonSdk personSdk,
            TicketSdk ticketSdk)
        {
            _ticketSdk = ticketSdk;
            _personSdk = personSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? assignedToId)
        {
            if (assignedToId.HasValue)
            {
                var assignedToPerson = await _personSdk.Get(assignedToId.Value);

                if (assignedToPerson is not null)
                {
                    //ViewBag.AssignedToPerson = assignedToPerson;
                    ViewData["AssignedToPerson"] = assignedToPerson;
                }
            }
            
            var tickets = await _ticketSdk.Find(assignedToId);

            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await GetCreateEditView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Ticket ticket)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return await GetCreateEditView("Create", ticket);
            }

            await _ticketSdk.Create(ticket);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var ticket = await _ticketSdk.Get(id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            return await GetCreateEditView("Edit", ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]Ticket ticket)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return await GetCreateEditView(nameof(Edit), ticket);
            }

            await _ticketSdk.Update(id, ticket);

            return RedirectToAction("Index");

        }

        private async Task<IActionResult> GetCreateEditView(string viewName, Ticket? ticket = null)
        {
            var people = await _personSdk.Find();
            ViewBag.People = people;
            return View(viewName, ticket);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketSdk.Get(id);

            return View(ticket);
        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _ticketSdk.Get(id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            await _ticketSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
