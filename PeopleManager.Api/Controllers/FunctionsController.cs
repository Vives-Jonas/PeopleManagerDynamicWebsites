using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FunctionsController(FunctionService functionService, ILogger<FunctionsController> logger) : ControllerBase
    {
        
        //FIND
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            logger.LogInformation("Find endpoint called");
            var result = await functionService.Find();
            return Ok(result);
        }


        //GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await functionService.Get(id);
            return Ok(result);
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FunctionRequest request)
        {
            var result = await functionService.Create(request);
            return Ok(result);
        }


        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FunctionRequest request)
        {
            var result = await functionService.Update(id, request);
            return Ok(result);
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
