using Application.DriverBL;
using AutoMapper;
using FluentValidation;
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
    public class Update
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

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<PostPriceDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var res = _context.Prices.FirstOrDefault(x => x.Id == request.Price.Id);
                    if (res != null)
                    {
                        _mapper.Map(request.Price, res);
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<PostPriceDto>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"Price Updated Successfully!",
                            Object = request.Price
                        };
                    }
                    return new ServiceStatus<PostPriceDto>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = $"Id Not Found!",
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
