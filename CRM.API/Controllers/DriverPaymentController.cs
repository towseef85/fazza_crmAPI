using Application.DriverPaymentBL;
using Domain.Drivers;
using Infrastructure.Dtos.AppUserDto;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.DriverPaymentDto;
using Infrastructure.Dtos.OrderDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class DriverPaymentController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDriverPaymentDto))]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{DriverId},{OrderPaymentStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UnpaidOrderList(Guid DriverId, string OrderPaymentStatus)
        {
            return HandleResult(await Mediator.Send(new UnpaidOrderList.Query { DriverId = DriverId, OrderPaymentStatus = OrderPaymentStatus }));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostDriverPaymentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PostDriverPaymentDto driverPayment)
        {
            return HandleResult(await Mediator.Send(new Create.Command { DriverPayment = driverPayment }));
        }
    }
}
