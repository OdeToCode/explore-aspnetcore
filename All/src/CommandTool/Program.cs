using System;

namespace CommandTool
{
    public class Program
    {
        public void Main(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}
