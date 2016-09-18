using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            foreach (var assembly in assemblies)
            {
                Console.WriteLine($"{assembly.Version} {assembly.Name}");

                var codebase = Assembly.Load(assembly).CodeBase.Replace("file:///", "");                               
                Console.WriteLine($"\t{Path.GetDirectoryName(codebase)}");
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
