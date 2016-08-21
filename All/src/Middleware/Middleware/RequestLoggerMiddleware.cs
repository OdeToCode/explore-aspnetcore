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
            Console.WriteLine("STart request");
            await _next(context);
            Console.WriteLine("End request");
        }

        RequestDelegate _next;
    }
}
