using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
