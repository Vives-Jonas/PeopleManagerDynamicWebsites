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
            var result = await personService.Find();
            return Ok(result);
        }


        //GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await personService.Get(id);
            return Ok(result);
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonRequest request)
        {
            var result = await personService.Create(request);
            return Ok(result);
        }


        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonRequest request)
        {
            var result = await personService.Update(id, request);
            return Ok(result);
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
