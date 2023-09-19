using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.AppUserDto;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppUserBL
{
    public class Create
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
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(ApplicationDbContext context, IMapper mapper,IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;

            }
            public async Task<ServiceStatus<PostAppUserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    request.AppUser.Id = Guid.NewGuid();
                    request.AppUser.CreatedUserId = request.AppUser.Id;

                    _context.AppUsers.Add(_mapper.Map<Domain.AppUsers.AppUser>(request.AppUser));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<PostAppUserDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"User Added Successfully!",
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
