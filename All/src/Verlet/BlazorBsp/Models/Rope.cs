using Blazor.Extensions.Canvas.Canvas2D;
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
            Particles = new List<Particle>(capacity: options.NumParticles + 1);

            var left = options.Left;
            var top = options.Top;
            var anchor = new AnchorParticle(left, top, options.ParticleSize);
            anchor.AddConstraint(new Constraint(anchor, options.ParticleDistance));
            Particles.Add(anchor);

            for(var i = 0; i < options.NumParticles; i++)
            {
                left += options.ParticleSize * 2;
                var particle = new Particle(left, top, options.ParticleSize);
                Particles.Add(particle);
                particle.AddConstraint(new Constraint(Particles.Last(), options.ParticleDistance));
            }
        }

        public async Task Draw(Canvas2DContext context)
        {
            foreach(var particle in Particles)
            {
                await context.FillRectAsync(particle.Left, particle.Top, particle.Width, particle.Height);
            }
        }

        public List<Particle> Particles { get; }
    }
}
