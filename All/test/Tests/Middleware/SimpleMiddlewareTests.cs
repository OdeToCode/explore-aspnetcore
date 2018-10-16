using Microsoft.AspNetCore.Http;
using Middleware;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Middleware
{
    public static class HttpContextExtensions
    {
        public static void AddLogItem(this HttpContext context, string message)
        {
            var log = GetLogItem(context);
            log.Add(message);
        }

        public static List<string> GetLogItem(this HttpContext context)
        {
            if (!context.Items.ContainsKey("log"))
            {
                context.Items.Add("log", new List<string>());
            }

            var log = context.Items["log"] as List<string>;           
            return log;
        }
    }

    public class SimpleMiddlewareTests
    {
        [Fact]
        public void CanBuildPipeline()
        {
            var app = new SimpleApplicationBuilder();

            app.Use(next =>
            {
                return async ctx =>
                {
                    ctx.AddLogItem("Enter middleware 1");
                    await next(ctx);
                    ctx.AddLogItem("Exit middleware 1");
                };
            });

            app.Use(next =>
            {
                return async ctx =>
                {
                    ctx.AddLogItem("Enter middleware 2");
                    await next(ctx);
                    ctx.AddLogItem("Exit middleware 2");
                };
            });

            app.Use(next =>
            {
                return async ctx =>
                {
                    ctx.AddLogItem("Enter middleware 3");
                    await next(ctx);
                    ctx.AddLogItem("Exit middleware 3");
                };
            });

            var pipeline = app.Build();

            var request = new TestHttpContext();
            pipeline(request);

            var log = request.GetLogItem();

            Assert.Equal(6, log.Count);
            Assert.Equal("Enter middleware 1", log[0]);
            Assert.Equal("Exit middleware 1", log[5]);
        }
    }
}
