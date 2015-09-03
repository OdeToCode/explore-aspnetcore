using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class SayHelloMiddleware
    {
        public SayHelloMiddleware(RequestDelegate next, SayHelloOptions options)
        {
            _options = options;
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return context.Response.WriteAsync(_options.GreetingText);
        }

        RequestDelegate _next;
        SayHelloOptions _options;
    }

    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseSayHello(this IApplicationBuilder app, 
                                                       SayHelloOptions options)
        {
            return app.UseMiddleware<SayHelloMiddleware>(options);
        }
    }
}
