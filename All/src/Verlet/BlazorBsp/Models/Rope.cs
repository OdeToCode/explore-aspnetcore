using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBsp.Models
{
    public class Rope
    {
        public Rope(RopeOptions options)
        {
            Particles = new List<Particle>(capacity: options.NumParticles);

            for(var i = 0; i < options.NumParticles; i++)
            {

            }
        }

        public List<Particle> Particles { get; }
    }
}
