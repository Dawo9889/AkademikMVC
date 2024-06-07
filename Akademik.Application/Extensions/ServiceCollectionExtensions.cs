﻿using Akademik.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IResidentService, ResidentService>();
        }
    }
}
