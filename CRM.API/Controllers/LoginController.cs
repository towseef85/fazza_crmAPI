using Application.LoginBL;
using Domain.AppUsers;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.LoginDto;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Dtos.UserDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.DataContexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace CRM.API.Controllers
{
    [Authorize(Roles ="Admin,Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseApiController
    {
        private readonly ApplicationDbContext _db;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IConfiguration configuration, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _config = configuration;
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginUserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginUserDto login)
        {

            var result = await Mediator.Send(new Login.Command { Login = login });
            return Ok(result);
        }

        [HttpGet("CurrentUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoggedInUserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LoggedInUserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return null;
            }
            return HandleResult(await Mediator.Send(new LoggedInUser.Query { Email = email }));
        }

        //[HttpGet]
        //public async Task<IActionResult> GetList()
        //{
        //    return HandleResult(await Mediator.Send(new List.Query { SubscriptionId = GetSubscriptionId() }));
        //}

        //protected Guid GetSubscriptionId()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;

        //    var value = identity.FindFirst("subscriptionId").Value;
        //    Guid SubscriptionId = new Guid(value);
        //    return SubscriptionId;

        //}
    }
}
