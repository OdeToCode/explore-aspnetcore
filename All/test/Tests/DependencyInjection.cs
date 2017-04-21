using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests
{
    public class DependencyInjectionWithOpenGenerics
    {
        [Fact]
        public void ItDoesWork()
        {
            var host = new WebHostBuilder()
                .UseServer(new EmptyServer())
                .UseStartup<Startup>();

            var services = host.Build().Services;

            Assert.IsType<SqlStore<User>>(services.GetRequiredService<IStore<User>>());
            Assert.IsType<SqlStore<Invoice>>(services.GetRequiredService<IStore<Invoice>>());
            Assert.IsType<SqlStore<Payment>>(services.GetRequiredService<IStore<Payment>>());
            Assert.Throws<ArgumentException>(() =>
            {
                services.GetRequiredService<IStore<int>>();
            });
        }

        class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddScoped<IStore<User>, SqlStore<User>>();
                services.AddScoped<IStore<Invoice>, SqlStore<Invoice>>();
                services.AddScoped<IStore<Payment>, SqlStore<Payment>>();

                services.AddScoped(typeof(IStore<>), typeof(SqlStore<>));
            }

            public void Configure() { }
        }

        class User { }
        class Invoice { }
        class Payment { }

        interface IStore<in T> 
        {
            string DoWork(T thing);
        }

        class SqlStore<T> : IStore<T> where T: class
        {
            public string DoWork(T thing)
            {
                return typeof(T).Name;
            }
        }
    }
}
