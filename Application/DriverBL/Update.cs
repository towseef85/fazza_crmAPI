using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverBL
{
    public class Update
    {
        public class Command : IRequest<ServiceStatus<DriverDto>>
        {
            public DriverDto Driver { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Driver).SetValidator(new DriverValidation());
            }
        }
        public class Handler : IRequestHandler<Command, ServiceStatus<DriverDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<DriverDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var res = _context.Drivers.FirstOrDefault(x => x.Id == request.Driver.Id);
                    if (res != null)
                    {
                        _mapper.Map(request.Driver, res);
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<DriverDto>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"Driver Updated Successfully!",
                            Object = request.Driver
                        };
                    }
                    return new ServiceStatus<DriverDto>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = $"Id Not Found!",
                        Object = request.Driver
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<DriverDto>
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
