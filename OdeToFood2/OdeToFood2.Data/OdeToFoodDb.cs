using Microsoft.EntityFrameworkCore;
using OdeToFood2.Core.Entities;

namespace OdeToFood2.Data
{
    public class OdeToFoodDb : DbContext
    {
        public OdeToFoodDb(DbContextOptions<OdeToFoodDb> options) : base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
