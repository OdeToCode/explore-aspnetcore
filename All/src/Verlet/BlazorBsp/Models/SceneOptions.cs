namespace BlazorBsp.Models
{
    public class SceneOptions
    {
        public int UpdatePeriod { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int NumParticles { get; set; }
        public int ParticleSize { get; set; }
        public int ParticleDistance { get; set; }
        public Force Gravity { get; set; }
        public Force Wind { get; set; }
        public int Iterations { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
    }
}
