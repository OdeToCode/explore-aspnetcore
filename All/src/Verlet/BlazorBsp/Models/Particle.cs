using System;
using System.Collections.Generic;

namespace BlazorBsp.Models
{
    public class Particle
    {
        public Particle(int left, int top, int particleSize, bool visible = true)
        {
            Visible = visible;
            Radius = particleSize;
            Current = new Point
            {
                X = left,
                Y = top
            };
            Last = Current.Copy();
            constraints = new List<Constraint>();
        }

        public void AddConstraint(Constraint constraint)
        {
            constraints.Add(constraint);
        }

        public void Apply(Point totalForce)
        {
            var scratch = Current.Copy();
            scratch.Subtract(Last);
            scratch.Add(totalForce);
            Last = Current.Copy();
            Current.Add(scratch);
        }

        public void SatisfyConstraints()
        {
            foreach (var constraint in constraints)
            {
                var dx = Current.X - constraint.Tether.Current.X;
                var dy = Current.Y - constraint.Tether.Current.Y;
                var distance = Math.Sqrt((dx * dx) + (dy * dy));
                if (distance != 0)
                {
                    distance = 0.5 * (distance - constraint.Distance) / distance;
                }

                dx = dx * distance;
                dy = dy * distance;

                Current.X += dx;
                Current.Y += dy;
                constraint.Tether.Current.X -= dx;
                constraint.Tether.Current.Y -= dy;

            }
        }

        public bool Visible { get; }
        public int Radius { get; }
        public Point Last { get; protected set; }
        public Point Current { get; protected set; }

        private readonly List<Constraint> constraints;
    }
}
