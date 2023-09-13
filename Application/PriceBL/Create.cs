using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PriceBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<PriceDto>>
        {
            public PriceDto Price { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Price).SetValidator(new PriceValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<PriceDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<PriceDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    request.Price.Id = Guid.NewGuid();
                    _context.Prices.Add(_mapper.Map<Domain.Prices.Price>(request.Price));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<PriceDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Price Added Successfully!",
                        Object = request.Price
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PriceDto>
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
