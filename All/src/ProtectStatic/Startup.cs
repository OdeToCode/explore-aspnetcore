using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;

namespace ProtectStatic
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            WebApplication.Run(args);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", p => p.RequireAuthenticatedUser());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment host, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Verbose;
            loggerFactory.AddConsole();


            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
            });

            app.Map("/account/login", loginApp =>
            {
                loginApp.Run(async ctx =>
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, "bob"));

                    await ctx.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
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
