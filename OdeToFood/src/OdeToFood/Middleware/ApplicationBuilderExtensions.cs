using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Microsoft.AspNet.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(
            this IApplicationBuilder app, 
            IApplicationEnvironment env)
        {

            var path = Path.Combine(env.ApplicationBasePath, "node_modules");
            var provider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            options.FileProvider = provider;

            app.UseStaticFiles(options);
            return app;
        }
    }
}
