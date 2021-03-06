﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.Hosting;

namespace WorkingMvc6
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("IsLucky", builder =>
                    {
                        var random = new Random();
                        builder.RequireAssertion(_ => random.Next(1,100) < 75);
                    });
                })
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                    options.CacheProfiles.Add("Aggressive", new CacheProfile { Duration = 60 });
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
        
        public void Configure(IApplicationBuilder app, 
                              IHostApplicationLifetime lifetime, 
                              ILoggerFactory loggerFactory, 
                              IWebHostEnvironment env)
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
