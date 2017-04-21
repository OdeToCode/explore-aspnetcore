using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Middleware.Middleware
{
    public class RequestLoggerMiddleware
    {
        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"Starting {context.TraceIdentifier} in {GetHashCode()}");           
            await _next(context);
            Console.WriteLine($"Ending {context.TraceIdentifier} in {GetHashCode()}");
        }

        RequestDelegate _next;
    }
}
