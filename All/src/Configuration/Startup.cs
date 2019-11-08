using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuation)
        {
            Configuration = configuation;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MessageConfiguration>(Configuration);
            services.AddSingleton<ISecretNumber, SecretNumber>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMiddleware<GreetingMiddleware>();
        }
    }
}
