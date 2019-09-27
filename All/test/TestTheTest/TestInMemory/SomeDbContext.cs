using Microsoft.EntityFrameworkCore;

namespace TestInMemory
{

    public class SomeDbContext : DbContext
    {
        public SomeDbContext(DbContextOptions<SomeDbContext> options)
            : base(options)
        {

        }

        public DbSet<SomeEntity> Entities { get; set; }
    }
}
