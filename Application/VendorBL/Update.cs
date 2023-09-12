using Application.VendorBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.VendorDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VendorBL
{
    public class Update
    {
        public class Command : IRequest<ServiceStatus<VendorDto>>
        {
            public VendorDto Vendor { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Vendor).SetValidator(new VendorValidation());
            }
        }
        public class Handler : IRequestHandler<Command, ServiceStatus<VendorDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<VendorDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var res = _context.Vendors.FirstOrDefault(x => x.Id == request.Vendor.Id);
                    if (res != null)
                    {
                        _mapper.Map(request.Vendor, res);
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<VendorDto>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"Vendor Updated Successfully!",
                            Object = request.Vendor
                        };
                    }
                    return new ServiceStatus<VendorDto>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = $"Id Not Found!",
                        Object = request.Vendor
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<VendorDto>
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
