using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Middleware.Middleware
{
    public class FaultyMiddleware
    {
        public FaultyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // saving the context so we don't need to pass around as a parameter
            this._context = context;

            DoSomeWork();

            await _next(context);            
        }

        private void DoSomeWork()
        {
            // code that calls other private methods
        }

        // ...

        HttpContext _context;
        RequestDelegate _next;
    }
}