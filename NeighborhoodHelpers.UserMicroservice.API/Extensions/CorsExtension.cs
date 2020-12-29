using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeighborhoodHelpers.UserMicroservice.API.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsExtension(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });
            return services;
        }
    }
}
