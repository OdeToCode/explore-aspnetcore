using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Configuration
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment, 
                       IApplicationEnvironment applicationEnvironment)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(applicationEnvironment.ApplicationBasePath)
                   .AddJsonFile("config.json")
                   .AddUserSecrets()
                   .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MessageConfiguration>(Configuration);
            services.AddLogging();
            services.AddScoped<ISecretNumber>(provider => new SecretNumber(Configuration));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseMiddleware<GreetingMiddleware>();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
