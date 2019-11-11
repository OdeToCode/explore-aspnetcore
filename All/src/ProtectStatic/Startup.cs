using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ProtectStatic
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                    .AddCookie();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", p => p.RequireAuthenticatedUser());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHost host, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            app.Map("/account/login", loginApp =>
            {
                loginApp.Run(async ctx =>
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, "bob"));

                    await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                    await ctx.Response.WriteAsync("You are logged in! Go to <a href='/secret/secretpage.html'>Secret page</a>");
                    return;
                });
            });

            app.UseProtectFolder(new ProtectFolderOptions
            {
                Path = "/Secret",
                PolicyName = "Authenticated"
            });
            app.UseStaticFiles();

            app.Run(context =>
            {
                return context.Response.WriteAsync("<a href='/secret/secretpage.html'>Secret page</a>");
            });
        }
    }
}
