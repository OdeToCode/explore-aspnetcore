using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using WorkingMvc6.Services;

namespace WorkingMvc6.Middleware
{
    public class CoutingMiddleware
    {
        private readonly RequestDelegate _next;

        public CoutingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IGreeter greeter)
        {
            await _next(context);
        }
    }
}
