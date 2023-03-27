using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Model;
using VivesHelpdesk.Ui.WebApp.Sdk;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonSdk _personSdk;

        public PeopleController(PersonSdk personSdk)
        {
            _personSdk = personSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _personSdk.Find();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Person person)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            await _personSdk.Create(person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var person = await _personSdk.Get(id);

            if(person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]Person person)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            await _personSdk.Update(id, person);

            return RedirectToAction("Index");

        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _personSdk.Get(id);

            if(person is null)
            {
                return RedirectToAction("Index");
            }

            await _personSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
