using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.DependencyInjection;

namespace TagMania
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {      
            app.UseMvc(rb =>
            {
                rb.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
       
        }
    }
}
