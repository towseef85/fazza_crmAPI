using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.DriverPaymentDto;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverPaymentBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<PostDriverPaymentDto>>
        {
            public PostDriverPaymentDto DriverPayment { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DriverPayment).SetValidator(new DriverPaymentValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<PostDriverPaymentDto>>
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
            public async Task<ServiceStatus<PostDriverPaymentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    request.DriverPayment.CreatedUserId = new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    request.DriverPayment.Id = Guid.NewGuid();

                    _context.DriverPayments.Add(_mapper.Map<Domain.Drivers.DriverPayment>(request.DriverPayment));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<PostDriverPaymentDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Payment Added Successfully!",
                        Object = request.DriverPayment
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostDriverPaymentDto>
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
