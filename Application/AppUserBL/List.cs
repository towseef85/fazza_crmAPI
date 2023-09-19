using AutoMapper;
using Infrastructure.Dtos.AppUserDto;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.UserDto;
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
    public class List
    {
        public class Query : IRequest<ServiceStatus<List<GetAppUserDto>>>
        {
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<List<GetAppUserDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<List<GetAppUserDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.AppUsers.ToArrayAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<List<GetAppUserDto>>(result);
                    return new ServiceStatus<List<GetAppUserDto>>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<List<GetAppUserDto>>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
