using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Services;
using Newtonsoft.Json.Serialization;

namespace Movies
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            Configuration = 
                new ConfigurationBuilder()
                    .SetBasePath(environment.WebRootPath)
                    .AddJsonFile("config.json")
                    .Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlServer()
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
            app.UseDeveloperExceptionPage();             
            app.UseMvc();
        }
    }
}
