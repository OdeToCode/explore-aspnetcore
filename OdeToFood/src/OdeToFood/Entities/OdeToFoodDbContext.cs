using Microsoft.Data.Entity;

namespace OdeToFood.Entities
{
    public class OdeToFoodDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
