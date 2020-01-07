using System.Collections.Generic;

namespace BlazorBsp.Models
{
    public class Particle
    {
        public Particle(int left, int top, int particleSize, bool visible = true)
        {
            Visible = visible;
            Width = particleSize;
            Height = particleSize;
            Left = left;
            Top = top;
            constraints = new List<Constraint>();
        }

        public void AddConstraint(Constraint constraint)
        {
            constraints.Add(constraint);
        }

        public int Width { get; }
        public bool Visible { get; }
        public int Height { get; }
        public int Left { get; protected set; } 
        public int Top { get; protected set; }

        private readonly List<Constraint> constraints;
    }
}
