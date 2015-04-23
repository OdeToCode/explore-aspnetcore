using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace EmptyStart
{
    public class Startup
    { 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=Index}");
            });
        } 
    }
}
