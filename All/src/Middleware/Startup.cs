using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using Middleware.Middleware;

namespace Middleware
{

    public class Startup
    {
        public static void Main(string[] args)
        {
            WebApplication.Run(args);
        }

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
