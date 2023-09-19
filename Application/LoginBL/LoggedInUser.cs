using AutoMapper;
using Infrastructure.Dtos.LoginDto;
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

namespace Application.LoginBL
{
    public class LoggedInUser
    {
        public class Query : IRequest<ServiceStatus<LoggedInUserDto>>
        {
            public string Email { get; set; }
            public LoggedInUserDto LoggedIn { get; set; }
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<LoggedInUserDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<LoggedInUserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.AppUsers.Where(x => x.Email == request.Email).FirstOrDefaultAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<LoggedInUserDto>(result);
                    return new ServiceStatus<LoggedInUserDto>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<LoggedInUserDto>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
