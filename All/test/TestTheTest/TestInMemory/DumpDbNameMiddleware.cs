using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TestInMemory
{
    public class DumpDbNameMiddleware
    {
        private readonly RequestDelegate next;

        public DumpDbNameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext ctx, SomeDbContext db)
        {
            if (ctx.Request.Path.StartsWithSegments("/db"))
            {
                await ctx.Response.WriteAsync(db.Database.ProviderName);
            }
            else
            {
                await next(ctx);
            }
        }
    }
}
