﻿using Blazor.Extensions.Canvas.Canvas2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBsp.Models
{
    public class Rope
    {
        private readonly SceneOptions options;

        public Rope(SceneOptions options)
        {
            Particles = new List<Particle>(capacity: options.ParticleCount + 1);

            var left = options.Left;
            var top = options.Top;
            var anchor = new AnchorParticle(left, top, options.ParticleSize);
            Particles.Add(anchor);

            for(var i = 0; i < options.ParticleCount; i++)
            {
                left += options.ParticleDistance;
                var particle = new Particle(left, top, options.ParticleSize);
                particle.AddConstraint(new Constraint(Particles.Last(), options.ParticleDistance));
                Particles.Add(particle);
            }

            this.options = options;
        }

        public void Verlet(Point totalForce)
        {
            foreach (var particle in Particles.Skip(1))
            {
                particle.Apply(totalForce);
            }

            for (var i = 0; i < options.Iterations; i++)
            {
                foreach (var particle in Particles.Skip(1))
                {
                    particle.SatisfyConstraints();
                }
            }

            foreach(var particle in Particles)
            {
                particle.Current.X = Math.Max(particle.Current.X, 0);
                particle.Current.X = Math.Min(particle.Current.X, options.Width);
                particle.Current.Y = Math.Max(particle.Current.Y, 0);
                particle.Current.Y = Math.Min(particle.Current.Y, options.Height);
            }
        }

        public async Task Draw(Canvas2DContext context)
        {
            await context.ClearRectAsync(0, 0, 1024, 768);

            foreach (var particle in Particles)
            {
                await context.BeginPathAsync();
                await context.ArcAsync(particle.Current.X, particle.Current.Y, particle.Radius, 0, Math.PI * 2);
                await context.FillAsync();
                await context.ClosePathAsync();
            }
        }

        public List<Particle> Particles { get; }
    }
}
