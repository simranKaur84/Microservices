using Microsoft.Extensions.DependencyInjection;
using NeighborhoodHelpers.UserMicroservice.API.Filters;
using NeighborhoodHelpers.UserMicroservice.DataAccessProvider.UserDataAccess;
using NeighborhoodHelpers.UserMicroservice.Entities.DatabaseContext;
using NeighborhoodHelpers.UserMicroservice.Entities.Utilities.ErrorHelper;
using NeighborhoodHelpers.UserMicroservice.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeighborhoodHelpers.UserMicroservice.API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDataAccess, UserDataAccess>();
            services.AddScoped<IUserDbContext, UserDbContext>();
            services.AddScoped<IErrorHelper, ErrorHelper>();
            services.AddScoped<ExceptionHandlerFilter>();
            return services;
        }
    }
}
