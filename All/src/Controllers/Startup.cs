using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
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
            app.UseMvc(rb =>
            {            
            });
        }

        public static void Main(string[] args)
        {
            WebApplication.Run(args);
        }
    }
}
