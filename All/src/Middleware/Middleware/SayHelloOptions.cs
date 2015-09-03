using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class SayHelloOptions
    {
        public SayHelloOptions()
        {
            GreetingText = "Hello, World!";
        }

        public string GreetingText { get; set; }
    }
}
