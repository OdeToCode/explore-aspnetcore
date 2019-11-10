using Controllers.Controllers;
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
                o.ModelMetadataDetailsProviders.Add(new MyBindingMetadataProvider());
                o.ModelBinderProviders.Insert(0, new EmbeddedJsonModelBinderProvider());
                o.InputFormatters.Add(new XmlSerializerInputFormatter(o));
                o.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
        }

        public void Configure(IApplicationBuilder app)
        {            
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });

            app.Run(async (context) =>
            {                
                await context.Response.WriteAsync("Not found");
            });

        }

        public IConfiguration Configuration { get; set; }
    }
}
