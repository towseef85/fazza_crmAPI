using AutoMapper;
using Infrastructure.Dtos.AppUserDto;
using Infrastructure.Dtos.DriverDto;
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

namespace Application.AppUserBL
{
    public class Details
    {
        public class Query : IRequest<ServiceStatus<GetAppUserDto>>
        {
            public Guid Id { get; set; }
            public GetAppUserDto AppUser { get; set; }
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<GetAppUserDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<GetAppUserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.AppUsers.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<GetAppUserDto>(result);
                    return new ServiceStatus<GetAppUserDto>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<GetAppUserDto>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
