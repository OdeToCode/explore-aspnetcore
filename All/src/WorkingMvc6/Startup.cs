using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using WorkingMvc6.Services;
using WorkingMvc6.Middleware;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System;

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

            services.AddScoped<IGreeter, Greeter>();
            services.AddSingleton(services);
        }
        
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime, 
                              ILoggerFactory loggerFactory, 
                              IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof (CoutingMiddleware));
            app.UseMiddleware(typeof(CoutingMiddleware));
                                                                 
            var clientPath = Path.Combine(env.ContentRootPath, "client");
            var fileprovider = new PhysicalFileProvider(clientPath);
            var fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions
                             .DefaultFileNames = new[] { "foo.html" };
            fileServerOptions.FileProvider = fileprovider;

            app.UseFileServer(fileServerOptions);

            app.UseCors("MyCors");
            app.UseMvc();

            app.Run(async context =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Route not found");
            });
        }       
    }   
}
