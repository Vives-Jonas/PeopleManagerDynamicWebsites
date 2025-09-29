using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers;

[Authorize]
public class PeopleController(PersonService personService, FunctionService functionService) : Controller
{
    

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var people = await personService.Find();
        return View(people);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await CreateView("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Create(Person person)
    {
        if (!ModelState.IsValid)
        {
            return await CreateView("Create", person);
        }
        await personService.Create(person);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var person = await personService.Get(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }

        return await CreateView("Edit", person);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] Person person)
    {
        if (!ModelState.IsValid)
        {
            return await CreateView("Edit", person);
        }

        await personService.Update(id, person);

        return RedirectToAction("Index");
    }



    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var person = await personService.Get(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }
        return View(person);
    }

    [HttpPost]
    [Route("[controller]/Delete/{id:int?}")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await personService.Delete(id);

        return RedirectToAction("Index");
    }


    private async Task<IActionResult> CreateView([AspMvcView] string viewName, Person? person = null)
    {
        var functions = await functionService.Find();
        ViewBag.Functions = functions;

        if (person is null)
        {
            return View(viewName);
        }
        return View(viewName, person);
    }

}
