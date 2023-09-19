using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.VendorDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.VendorBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<PostVendorDto>>
        {
            public PostVendorDto Vendor { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Vendor).SetValidator(new VendorValidation());
            }
        }
        public class Handler : IRequestHandler<Command, ServiceStatus<PostVendorDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ServiceStatus<PostVendorDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    request.Vendor.CreatedUserId = new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    request.Vendor.Id = Guid.NewGuid();
                    _context.Vendors.Add(_mapper.Map<Domain.Vendors.Vendor>(request.Vendor));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<PostVendorDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Vendor Added Successfully!",
                        Object = request.Vendor
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostVendorDto>
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
