using Microsoft.AspNet.Builder;
using Microsoft.AspNet.StaticFiles;

namespace WithJspm
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseFileServer(new FileServerOptions
            {
                 EnableDefaultFiles = true,
                 EnableDirectoryBrowsing = true,                   
            });            
        }
    }
}
