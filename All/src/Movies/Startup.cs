using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Services;
using Newtonsoft.Json.Serialization;
using static Microsoft.Azure.KeyVault.KeyVaultClient;

namespace Movies
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services               
                .AddEntityFrameworkSqlServer()
                .AddDbContext<MovieDb>(options =>
                {
                    var connection = Configuration["Data:DefaultConnection:ConnectionString"];
                    options.UseSqlServer(connection);
                });

            services
                .AddMvc();

            services.AddSingleton(sp =>
            {
                var tokenProvider = new AzureServiceTokenProvider();
                var callback = new AuthenticationCallback(tokenProvider.KeyVaultTokenCallback);
                return new KeyVaultClient(callback);
            });
                
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();             
            app.UseMvc();
        }
    }
}
