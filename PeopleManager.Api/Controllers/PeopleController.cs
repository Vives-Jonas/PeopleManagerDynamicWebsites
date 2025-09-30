using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(PersonService personService) : ControllerBase
    {
        //FIND
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var people = await personService.Find();
            return Ok(people);
        }


        //GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var person = await personService.Get(id);
            return Ok(person);
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonRequest request)
        {
            var person = await personService.Create(request);
            return Ok(person);
        }


        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonRequest request)
        {
            var person = await personService.Update(id, request);
            return Ok(person);
        }


        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await personService.Delete(id);
            return Ok();
        }
    }
}
