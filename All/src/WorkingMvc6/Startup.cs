using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using WorkingMvc6.Services;
using Microsoft.AspNet.Mvc.Razor.Compilation;
using WorkingMvc6.Controllers;
using WorkingMvc6.Middleware;

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

                    options.CacheProfiles.Add("Aggressive", new CacheProfile {Duration = 60});
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddViewOptions(options =>
                {
                    options.HtmlHelperOptions.ClientValidationEnabled = false;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("MyCors", configure =>
                {
                    configure.WithOrigins("*").WithMethods("GET");
                });
            });

            services.AddSingleton<IRazorCompilationService, RazorCompilationServiceSpy>();
            services.AddScoped<IGreeter, Greeter>();
            services.AddInstance(services);
        }
        
        public void Configure(IApplicationBuilder app, 
                              ILoggerFactory loggerFactory, 
                              IHostingEnvironment env)
        {
            loggerFactory.AddDebug();
            loggerFactory.MinimumLevel = LogLevel.Verbose;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof (CoutingMiddleware));
            app.UseMiddleware(typeof(CoutingMiddleware));

            app.UseCors("MyCors");
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
