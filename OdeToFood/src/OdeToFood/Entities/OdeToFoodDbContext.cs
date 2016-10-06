using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OdeToFood.Entities
{
    public class OdeToFoodDbContext : IdentityDbContext<User>
    {

        public OdeToFoodDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public void Initialize()
        {
            //Database.EnsureCreated();   Doesn't take migrations into account
            Database.Migrate();
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
