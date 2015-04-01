using Microsoft.AspNet.Builder;

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
