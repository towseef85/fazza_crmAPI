using AutoMapper;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.DriverPaymentDto;
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

namespace Application.DriverPaymentBL
{
    public class List
    {
        public class Query : IRequest<ServiceStatus<List<GetDriverPaymentDto>>>
        {
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<List<GetDriverPaymentDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<List<GetDriverPaymentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.DriverPayments.ToArrayAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<List<GetDriverPaymentDto>>(result);
                    return new ServiceStatus<List<GetDriverPaymentDto>>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<List<GetDriverPaymentDto>>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
