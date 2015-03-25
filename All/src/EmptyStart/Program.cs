using Microsoft.Framework.DependencyInjection.ServiceLookup;
using System;

namespace EmptyStart
{
    public class Program
    {
        public Program(IServiceManifest manifest)
        {
            
        }

        public static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}