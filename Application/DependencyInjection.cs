﻿using Application.Driver;
using Application.DriverBL;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Create.Command).Assembly);
            services.AddMediatR(typeof(List.Query).Assembly);

            return services;
        }
    }
}
