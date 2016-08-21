using Microsoft.EntityFrameworkCore;
using Movies.Entities;

namespace Movies.Services
{
    public class MovieDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
