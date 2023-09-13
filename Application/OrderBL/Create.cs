﻿using Application.DriverBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<PostOrderDto>>
        {
            public PostOrderDto Order { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Order).SetValidator(new OrderValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<PostOrderDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<PostOrderDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    request.Order.Id = Guid.NewGuid();
                    _context.Orders.Add(_mapper.Map<Domain.Orders.Order>(request.Order));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<PostOrderDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Order Added Successfully!",
                        Object = request.Order
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostOrderDto>
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
