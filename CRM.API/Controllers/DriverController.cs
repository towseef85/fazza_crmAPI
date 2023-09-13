
using Application.Driver;
using Application.DriverBL;
using Infrastructure.Dtos.DriverDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDriverDto))]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDriverDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid Id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = Id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostDriverDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PostDriverDto driver)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Driver = driver }));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDriverDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(PostDriverDto driver)
        {
            return HandleResult(await Mediator.Send(new Update.Command { Driver = driver }));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = Id }));
        }
    }
}
