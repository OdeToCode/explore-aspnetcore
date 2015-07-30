using Microsoft.Framework.ConfigurationModel;
using System;
using Microsoft.Framework.Configuration;

namespace EmptyStart
{
    public class Program
    {   
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder(".")
                            .AddJsonFile("config.json")
                            .AddCommandLine(args)
                            .Build();

            Console.WriteLine(config.Get("message"));

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            Console.ReadLine();
        }
    }
}