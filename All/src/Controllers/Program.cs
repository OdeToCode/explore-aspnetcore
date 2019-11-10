using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
