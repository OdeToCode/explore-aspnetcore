namespace BlazorBsp.Models
{
    public class Constraint
    {
        public Constraint(Particle tether, int distance)
        {
            Tether = tether;
            Distance = distance;
        }

        public Particle Tether { get; }
        public int Distance { get; }
    }
}
