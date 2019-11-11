using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WorkingMvc6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost
                .CreateDefaultBuilder()
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
