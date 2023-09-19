using AutoMapper;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverBL
{
    public class Delete
    {
        public class Command : IRequest<ServiceStatus<Guid>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, ServiceStatus<Guid>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(ApplicationDbContext context, IMapper mapper,IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;

            }
            public async Task<ServiceStatus<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var res = _context.Drivers.FirstOrDefault(x => x.Id == request.Id);
                    _context.Drivers.Remove(res);
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<Guid>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Driver Deleted Successfully!",
                        Object = request.Id
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<Guid>
                    {
                        Code = System.Net.HttpStatusCode.InternalServerError,
                        Message = ex.Message.ToString(),
                        InnerMessage = exception.InnerException != null ? exception.InnerException.ToString() : "",
                        Object = request.Id
                    };
                }

            }
        }
    }
}
