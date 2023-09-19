using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Dtos.PriceDto;
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

namespace Application.PriceBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<PostPriceDto>>
        {
            public PostPriceDto Price { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Price).SetValidator(new PriceValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<PostPriceDto>>
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
            public async Task<ServiceStatus<PostPriceDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    request.Price.CreatedUserId = new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    request.Price.Id = Guid.NewGuid();

                    _context.Prices.Add(_mapper.Map<Domain.Prices.Price>(request.Price));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<PostPriceDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Price Added Successfully!",
                        Object = request.Price
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostPriceDto>
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
