using System;
using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Movies.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Dnx.Runtime;

namespace Movies
{
    public class Startup
    {
        public Startup(IApplicationEnvironment environment)
        {
            Configuration = 
                new ConfigurationBuilder(environment.ApplicationBasePath)
                    .AddJsonFile("config.json")
                    .Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<MovieDb>(options =>
                {
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                });

            services
                .AddMvc()
                .AddJsonOptions(o => o.SerializerSettings.ContractResolver = 
                                new CamelCasePropertyNamesContractResolver());
                
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();             
            app.UseMvc();
        }
    }
}
