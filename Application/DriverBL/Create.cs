using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Driver
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<PostDriverDto>>
        {
            public PostDriverDto Driver { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Driver).SetValidator(new DriverValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<PostDriverDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;

            }
            public async Task<ServiceStatus<PostDriverDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    request.Driver.CreatedUserId = new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    request.Driver.Id = Guid.NewGuid();

                    _context.Drivers.Add(_mapper.Map<Domain.Drivers.Driver>(request.Driver));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<PostDriverDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Driver Added Successfully!",
                        Object = request.Driver
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostDriverDto>
                    {
                        Code = System.Net.HttpStatusCode.InternalServerError,
                        Message = ex.Message.ToString(),
                        InnerMessage = exception.InnerException != null ? exception.InnerException.ToString() : "",
                        Object = null
                    };
                }
            }
        }
    }
}
