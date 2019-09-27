using System.Threading.Tasks;
using Xunit;

namespace TestInMemory
{
    public class TestStartup : IClassFixture<CustomWebFactory>
    {
        private readonly CustomWebFactory factory;

        public TestStartup(CustomWebFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task CanGetServiceResponse()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("/db");
            response.EnsureSuccessStatusCode();
            var text = await response.Content.ReadAsStringAsync();
            Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", text);
        }

        [Fact]
        public async Task CanSeeHello()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("/hi");
            response.EnsureSuccessStatusCode();
            var text = await response.Content.ReadAsStringAsync();
            Assert.Equal("hello", text);
        }


        [Fact]
        public void TrueIsGood()
        {
            Assert.True(true);
        }
    }
}
