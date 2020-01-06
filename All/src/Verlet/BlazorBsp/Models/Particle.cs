using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBsp.Models
{
    public class Particle
    {
        public int Width { get; } = 10;
        public int Height { get; } = 10;
        public int Left { get; protected set; } = 0;
        public int Top { get; protected set; } = 0;
    }
}
