using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NeighborhoodHelpers.UserMicroservice.API.Extensions;
using NeighborhoodHelpers.UserMicroservice.Entities.DatabaseContext;
using NeighborhoodHelpers.UserMicroservice.Entities.Utilities.AutoMapper;

namespace NeighborhoodHelpers.UserMicroservice.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            /* Add Services */
            services.AddServiceExtensions();

            /* Add controllers */
            services.AddControllers();

            /* Cors Extension */
            services.AddCorsExtension();

            /* Swagger Setup */
            services.AddSwaggerDocumentation();

            /* Add Mvc Extensions */
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddHttpContextAccessor();
            services.AddDbContext<UserDbContext>();

            /* Dynamo Db Setup */
            var dynamoDbConfig = Configuration.GetSection("DynamoDb");
            var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");

            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var clientConfig = new AmazonDynamoDBConfig
                {
                    ServiceURL = dynamoDbConfig.GetValue<string>("LocalServiceUrl")
                };
                return new AmazonDynamoDBClient(clientConfig);
            });

            /* AutoMapper Configuration */
            services.AddAutoMapper(typeof(AutoMapperProfileConfiguration).GetTypeInfo().Assembly);

            services.AddMvcCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }
            app.UseCors("CorsPolicy");
            app.UseSwaggerDocumentation();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc();
        }
    }
}
