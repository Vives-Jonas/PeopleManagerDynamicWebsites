using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Services;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers;

public class HomeController(PersonService personService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var people = await personService.Find();

        return View(people);
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
