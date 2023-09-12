using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;


#pragma warning disable CS8603 // Possible null reference return.
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
#pragma warning restore CS8603 // Possible null reference return.

        protected ActionResult HandleResult<T>(ServiceStatus<T> result)
        {
           // bool IsSecureEnvironment = true;

            //var result = task.ConfigureAwait(false);
            //HttpContext.Response.Headers.Add("X-Frame-Options", "deny");
            //HttpContext.Response.Headers.Add("X-Xss-Protection", "1");
            //HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //HttpContext.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
            //contextLogger.Commit(EventLogEntryType.SuccessAudit, Guid.NewGuid().ToString());
            if (result.Object == null)
            {

                // Work on Event Logging

                return new ObjectResult(new
                {
                    message = result.Message,
                    innerMessage = result.InnerMessage,
                })
                {
                    StatusCode = (int)result.Code
                };
            }
            else
            {
                return new ObjectResult(new
                {
                    StatusCode = (int)result.Code,
                    message = result.Message,
                    data = result.Object


                })
                { StatusCode = (int)result.Code };
            }

        }

    }
}
