using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Middleware.Middleware;

namespace Middleware
{

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggerMiddleware>();

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".foo", "text/plain");

            app.UseStaticFiles(new StaticFileOptions()
            {
                //ServeUnknownFileTypes = true
                ContentTypeProvider = provider 
            });
            app.UseDeveloperExceptionPage();            
            app.UseSayHello(new SayHelloOptions());                   
        }
    }
}
