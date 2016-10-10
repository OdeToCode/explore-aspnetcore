using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Controllers
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(o =>
            {
                o.InputFormatters.Add(new XmlSerializerInputFormatter());
                o.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
        }

        public void Configure(IApplicationBuilder app)
        {            
            app.UseDeveloperExceptionPage();
            app.UseMvc();

            app.Run(async (context) =>
            {
                var message = Configuration["Greeting"];
                await context.Response.WriteAsync(message);
            });

        }

        public IConfiguration Configuration { get; set; }
    }
}
