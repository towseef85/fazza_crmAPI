using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.VendorPriceDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VendorPricesBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<VendorPriceDto>>
        {
            public VendorPriceDto VendorPrice { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.VendorPrice).SetValidator(new VendorPriceValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<VendorPriceDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;


            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ServiceStatus<VendorPriceDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    request.VendorPrice.Id = Guid.NewGuid();
                    _context.VendorPrices.Add(_mapper.Map<Domain.VendorPrices.VendorPrice>(request.VendorPrice));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<VendorPriceDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Vendor Price Added Successfully!",
                        Object = request.VendorPrice
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<VendorPriceDto>
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
