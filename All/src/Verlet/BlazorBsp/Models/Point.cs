using System;

namespace BlazorBsp.Models
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public void Add(Point other)
        {
            X = X + other.X;
            Y = Y + other.Y; 
        }

        public Point Copy()
        {
            return new Point
            {
                X = X,
                Y = Y
            };
        }

        public void Subtract(Point other)
        {
            X = X - other.X;
            Y = Y - other.Y;
        }
    }
}
