using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace OdeToFood.Entities
{
    public class OdeToFoodDbContext : IdentityDbContext<User>
    {
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}