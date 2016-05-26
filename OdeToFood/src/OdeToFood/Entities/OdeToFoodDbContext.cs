using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Entities
{
    public class OdeToFoodDbContext : IdentityDbContext<User>
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}