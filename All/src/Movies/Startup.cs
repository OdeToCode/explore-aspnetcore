using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Movies.Services;
using Newtonsoft.Json.Serialization;

namespace Movies
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            WebApplication.Run(args);
        }

        public Startup(IApplicationEnvironment environment)
        {
            Configuration = 
                new ConfigurationBuilder()
                    .SetBasePath(environment.ApplicationBasePath)
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
            app.UseDeveloperExceptionPage();             
            app.UseMvc();
        }
    }
}
