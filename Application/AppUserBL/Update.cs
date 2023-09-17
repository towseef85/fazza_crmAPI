using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.AppUserDto;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppUserBL
{
    public class Update
    {
        public class Command : IRequest<ServiceStatus<PostAppUserDto>>
        {
            public PostAppUserDto AppUser { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AppUser).SetValidator(new AppUserValdation());
            }
        }
        public class Handler : IRequestHandler<Command, ServiceStatus<PostAppUserDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<PostAppUserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var res = _context.AppUsers.FirstOrDefault(x => x.Id == request.AppUser.Id);
                    if (res != null)
                    {
                        _mapper.Map(request.AppUser, res);
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<PostAppUserDto>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"User Updated Successfully!",
                            Object = request.AppUser
                        };
                    }
                    return new ServiceStatus<PostAppUserDto>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = $"Id Not Found!",
                        Object = request.AppUser
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostAppUserDto>
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
