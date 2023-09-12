﻿using AutoMapper;
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

namespace Application.DriverBL
{
    public class List
    {
        public class Query : IRequest<ServiceStatus<List<GetDriverDto>>>
        {
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<List<GetDriverDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<List<GetDriverDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Drivers.ToArrayAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<List<GetDriverDto>>(result);
                    return new ServiceStatus<List<GetDriverDto>>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<List<GetDriverDto>>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
