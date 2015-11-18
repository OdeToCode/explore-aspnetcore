using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.OptionsModel;

namespace Configuration
{
    public class GreetingMiddleware
    {
        readonly RequestDelegate _next;

        public GreetingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, 
            ISecretNumber secretNumber,
            IOptions<MessageConfiguration> configuration)
        {
            await context.Response.WriteAsync(secretNumber.ComputeNumber().ToString());
            await context.Response.WriteAsync(configuration.Value.Messages.Salutation);
        }
    }
}