using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace OdeToFood2.Data
{
    class DevelopmentDbContextFactory : IDesignTimeDbContextFactory<OdeToFoodDb>
    {
        public OdeToFoodDb CreateDbContext(string[] args)
        {
            foreach(var arg in args)
            {
                Console.WriteLine(arg);
            }

            var options = new DbContextOptionsBuilder<OdeToFoodDb>()
                            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OdeToFood2;Integrated Security=True;")
                            .Options;
            return new OdeToFoodDb(options);
        }
    }
}
