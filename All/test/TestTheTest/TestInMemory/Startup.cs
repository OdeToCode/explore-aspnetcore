using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestInMemory
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SomeDbContext>(options =>
            {
                options.UseSqlServer("fail");
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseMiddleware<DumpDbNameMiddleware>();
            app.UseEndpoints(e =>
            {
                e.MapGet("/hi", async ctx => await ctx.Response.WriteAsync("hello"));
            });
        }
    }
}
