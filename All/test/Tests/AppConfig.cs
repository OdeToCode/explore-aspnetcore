using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Tests
{
    public class AppConfig
    {
        [Fact]
        public void CanReadAnArray()
        {           
            var host = new WebHostBuilder()
                .UseServer(new EmptyServer())
                .UseStartup<Startup>();

            var services = host.Build().Services;

            var options = services.GetRequiredService<IOptions<Config>>();

            Assert.NotNull(options);
            Assert.NotNull(options.Value);
            Assert.Equal(25, options.Value.Storage.TimeOut);
            Assert.NotNull(options.Value.Storage.Blobs);
            Assert.Equal(2, options.Value.Storage.Blobs.Length);
            Assert.Equal("Secondary", options.Value.Storage.Blobs[1].Name);
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.AddOptions();
                services.Configure<Config>(config);
            }

            public void Configure()
            {
                
            }
        }


        public class Config
        {
            public Storage Storage { get; set; }            
        }

        public class Storage
        {
            public int TimeOut { get; set; }
            public BlobSettings[] Blobs { get; set; }
        }

        public class BlobSettings
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }


    }
}
