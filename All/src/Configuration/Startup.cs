using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration
{




    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(environment.WebRootPath)
                   .AddJsonFile("config.json")
                   .AddUserSecrets<Startup>()
                   .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //services.AddOptions<MessageConfiguration>(Configuration);
            services.AddLogging();
            services.AddScoped<ISecretNumber>(provider => new SecretNumber(Configuration));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<GreetingMiddleware>();
        }
    }
}
