using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VivesHelpdesk.Ui.WebApp.Models;
using VivesHelpdesk.Ui.WebApp.Sdk;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TicketSdk _ticketSdk;

        public HomeController(
            TicketSdk ticketSdk)
        {
            _ticketSdk = ticketSdk;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketSdk.Find();
            return View(tickets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

