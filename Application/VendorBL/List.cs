using AutoMapper;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.VendorDto;
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

namespace Application.VendorBL
{
    public class List
    {
        public class Query : IRequest<ServiceStatus<List<GetVendorDto>>>
        {
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<List<GetVendorDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<List<GetVendorDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Vendors.ToArrayAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<List<GetVendorDto>>(result);
                    return new ServiceStatus<List<GetVendorDto>>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<List<GetVendorDto>>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
