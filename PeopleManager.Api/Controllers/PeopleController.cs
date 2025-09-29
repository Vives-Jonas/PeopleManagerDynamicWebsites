using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(PersonService personService) : ControllerBase
    {
        //FIND
        [HttpGet]
        public IActionResult Find()
        {
            var people = personService.Find();
            return Ok(people);
        }


        //GET
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute]int id)
        {
            var people = personService.Get(id);
            return Ok(people);
        }


        //CREATE
        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            var newPerson = personService.Create(person);
            return Ok(newPerson);
        }


        //UPDATE
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Person person)
        {
            var updatedPerson = personService.Update(id, person);
            return Ok(updatedPerson);
        }


        //DELETE
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            personService.Delete(id);
            return Ok();
        }
    }
}
