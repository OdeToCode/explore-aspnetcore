using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Runtime;
using System;

namespace EmptyStart
{
    public class Program
    {
        public Program(ILibraryManager libraryManager)
        {
            foreach (var lib in libraryManager.GetLibraries())
            {
                Console.WriteLine(lib.Name);
            }
        }

        public static void Main(string[] args)
        {
            var config = new Configuration()
                            .AddJsonFile("config.json")
                            .AddCommandLine(args);

            Console.WriteLine(config.Get("message"));

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            Console.ReadLine();
        }
    }
}