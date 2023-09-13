
using Application.VendorPricesBL;
using Infrastructure.Dtos.VendorPriceDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorPriceController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetVendorPriceDto))]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetVendorPriceDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid Id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = Id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostVendorPriceDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PostVendorPriceDto vendorPrice)
        {
            return HandleResult(await Mediator.Send(new Create.Command { VendorPrice = vendorPrice }));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostVendorPriceDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(PostVendorPriceDto vendorPrice)
        {
            return HandleResult(await Mediator.Send(new Update.Command { VendorPrice = vendorPrice }));
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
