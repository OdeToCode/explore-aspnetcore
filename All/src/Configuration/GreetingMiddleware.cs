using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


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
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (configuration.Value == null) throw new ArgumentException(nameof(configuration.Value));
            if (configuration.Value.Messages == null) throw new ArgumentException(nameof(configuration.Value.Messages));
            if (configuration.Value.Messages.Salutation == null) throw new ArgumentException(nameof(configuration.Value.Messages.Salutation));

            await context.Response.WriteAsync(configuration.Value.Messages.Salutation);
            await context.Response.WriteAsync(Environment.NewLine);
            await context.Response.WriteAsync(secretNumber.ComputeNumber().ToString());
        }
    }
}