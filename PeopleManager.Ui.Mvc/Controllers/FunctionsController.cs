using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;


namespace PeopleManager.Ui.Mvc.Controllers
{
   
    public class FunctionsController(FunctionClient functionClient) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var functions = await functionClient.Find();
            return View(functions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FunctionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await functionClient.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var function = await functionClient.Get(id);
            if (function is null)
            {
                return RedirectToAction("Index");
            }
            var request = new FunctionRequest
            {
                Name = function.Name,
                Description = function.Description
            };

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]FunctionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await functionClient.Update(id, request);

            return RedirectToAction("Index");
        }

        

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public async Task<IActionResult> Delete(int id)
        {
            await functionClient.Delete(id);

            return RedirectToAction("Index");
        }
    }
}

