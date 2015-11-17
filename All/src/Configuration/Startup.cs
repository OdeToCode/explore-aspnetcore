using System.Collections.Generic;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;

namespace Configuration
{

    public interface IConfigurationSource
    {
        bool TryGet(string key, out string value);

        void Set(string key, string value);

        void Load();

        IEnumerable<string> ProduceConfigurationSections(
            IEnumerable<string> earlierKeys,
            string prefix,
            string delimiter);
    }


    public class Startup
    {
        public Startup(IHostingEnvironment environment, 
                       IApplicationEnvironment applicationEnvironment)
        {
            var builder = new ConfigurationBuilder(applicationEnvironment.ApplicationBasePath);
            builder.AddJsonFile("config.json")
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
            app.UseMiddleware<GreetingMiddleware>();
        }
    }
}
