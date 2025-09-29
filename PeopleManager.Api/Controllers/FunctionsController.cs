using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController(FunctionService functionService) : ControllerBase
    {
        //FIND
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var functions = await functionService.Find();
            return Ok(functions);
        }


        //GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var function = await functionService.Get(id);
            return Ok(function);
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Function function)
        {
            var newFunction = await functionService.Create(function);
            return Ok(newFunction);
        }


        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Function function)
        {
            var updatedFunction = await functionService.Update(id, function);
            return Ok(updatedFunction);
        }


        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await functionService.Delete(id);
            return Ok();
        }
    }
}
