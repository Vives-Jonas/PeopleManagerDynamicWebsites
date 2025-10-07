using Azure.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;
using PeopleManager.Ui.Mvc.Extensions;

namespace PeopleManager.Ui.Mvc.Controllers;

//[Authorize]
public class PeopleController(PersonClient personClient, FunctionClient functionClient) : Controller
{
    

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var people = await personClient.Find();
        return View(people);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await CreateView("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Create(PersonRequest request)
    {
        if (!ModelState.IsValid)
        {
            return await CreateView("Create", request);
        }
        var result = await personClient.Create(request);
        if (!result.IsSuccess)
        {
            ModelState.AddServiceMessages(result.Messages);
            return await CreateView("Create", request);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var person = await personClient.Get(id);
        if (person is null)
        {
            return RedirectToAction("Index");
        }

        var request = new PersonRequest
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            FunctionId = person.FunctionId
        };

        return await CreateView("Edit", request);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] PersonRequest request)
    {
        if (!ModelState.IsValid)
        {
            return await CreateView("Edit", request);
        }

        
        var result = await personClient.Update(id, request);

        if (!result.IsSuccess)
        {
            ModelState.AddServiceMessages(result.Messages);
            return await CreateView("Edit", request);
        }

        return RedirectToAction("Index");
    }
    

    

    [HttpPost]
    [Route("[controller]/Delete/{id:int?}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await personClient.Delete(id);
        if (!result.IsSuccess)
        {
            ModelState.AddServiceMessages(result.Messages);
            return View();
        }

        return RedirectToAction("Index");
    }


    private async Task<IActionResult> CreateView([AspMvcView] string viewName, PersonRequest? request = null)
    {
        var functions = await functionClient.Find();
        ViewBag.Functions = functions;

        if (request is null)
        {
            return View(viewName);
        }
        return View(viewName, request);
    }

}
