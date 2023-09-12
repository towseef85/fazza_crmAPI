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
    public class Details
    {
        public class Query : IRequest<ServiceStatus<GetVendorDto>>
        {
            public Guid Id { get; set; }
            public GetVendorDto Vendor { get; set; }
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<GetVendorDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<GetVendorDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Vendors.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<GetVendorDto>(result);
                    return new ServiceStatus<GetVendorDto>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<GetVendorDto>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
