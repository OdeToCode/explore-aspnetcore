using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Logging;
using Middleware.Middleware;

namespace Middleware
{

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment environment,
                              ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<RequestLoggerMiddleware>();
            app.UseStaticFiles();
            app.UseErrorPage();
            //app.Run(context =>
            //{
            //    throw new System.Exception("Boom!");
            //});
            app.UseSayHello(new SayHelloOptions());                   
        }
    }
}
