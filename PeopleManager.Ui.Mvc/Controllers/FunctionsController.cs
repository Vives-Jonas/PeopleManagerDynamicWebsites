using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers
{
    [Authorize]
    public class FunctionsController(FunctionService functionService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var functions = await functionService.Find();
            return View(functions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Function function)
        {
            if (!ModelState.IsValid)
            {
                return View(function);
            }

            await functionService.Create(function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var function = await functionService.Get(id);
            if (function is null)
            {
                return RedirectToAction("Index");
            }
            return View(function);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]Function function)
        {
            if (!ModelState.IsValid)
            {
                return View(function);
            }

            await functionService.Update(id, function);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var function = await functionService.Get(id);
            if (function is null)
            {
                return RedirectToAction("Index");
            }
            return View(function);
        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await functionService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}

