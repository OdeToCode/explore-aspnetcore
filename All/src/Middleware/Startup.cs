using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            app.UseDeveloperExceptionPage();
            //app.Run(context =>
            //{
            //    throw new System.Exception("Boom!");
            //});
            app.UseSayHello(new SayHelloOptions());                   
        }
    }
}
