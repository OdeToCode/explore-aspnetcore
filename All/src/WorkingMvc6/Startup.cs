using System;
using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using WorkingMvc6.Controllers;

namespace WorkingMvc6
{
    public class Startup
    { 
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddViewOptions(options =>
                {
                    options.HtmlHelperOptions.ClientValidationEnabled = false;
                });
                               
            services.AddSingleton<IServiceCollection>(provider => services);
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug();
            loggerFactory.MinimumLevel = LogLevel.Verbose;

            app.UseDeveloperExceptionPage();

            app.UseMvc();

            app.Run(async context =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Route not found");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
