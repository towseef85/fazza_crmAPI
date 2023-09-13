using AutoMapper;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.PriceBL
{
    public class Details
    {
        public class Query : IRequest<ServiceStatus<GetPriceDto>>
        {
            public Guid Id { get; set; }
            public GetPriceDto Driver { get; set; }
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<GetPriceDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<GetPriceDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Prices.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<GetPriceDto>(result);
                    return new ServiceStatus<GetPriceDto>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<GetPriceDto>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
