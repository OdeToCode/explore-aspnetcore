using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost
                .CreateDefaultBuilder()
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(b => {
                    b.AddUserSecrets<Startup>();
                    b.AddJsonFile("config.json", optional: false);
                 })
                .Build();
            host.Run();
        }
    }
}
