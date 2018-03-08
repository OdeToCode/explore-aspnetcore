using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.KeyVault;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Movies.Services;
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

            //services.AddSingleton(sp =>
            //{
            //    var tokenProvider = new AzureServiceTokenProvider();
            //    var callback = new AuthenticationCallback(tokenProvider.KeyVaultTokenCallback);
            //    return new KeyVaultClient(callback);
            //});
            services.AddSingleton<IKeyVaultCrypto>(sp =>
            {
                AuthenticationCallback callback = async (authority,resource,scope) =>
                {
                    var appId = Configuration["AppId"];
                    var appSecret = Configuration["AppSecret"];
                    var authContext = new AuthenticationContext(authority);
                    var credential = new ClientCredential(appId, appSecret);
                    var authResult = await authContext.AcquireTokenAsync(resource, credential);
                    return authResult.AccessToken;
                };
                
                var client = new KeyVaultClient(callback);
                return new KeyVaultCrypto(client, Configuration["KeyId"]);
            });
                
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();             
            app.UseMvc();
        }
    }
}
