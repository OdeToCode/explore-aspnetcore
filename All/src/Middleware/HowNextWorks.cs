using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Middleware
{
    public class TestHttpContext : HttpContext
    {
        public TestHttpContext()
        {
            Items = new Dictionary<object, object>();
        }

        public override IFeatureCollection Features => throw new NotImplementedException();

        public override HttpRequest Request => throw new NotImplementedException();

        public override HttpResponse Response => throw new NotImplementedException();

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();
        

        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IDictionary<object, object> Items { get; set; }
        public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }

    public class MiddlewareComponentNode
    {
        public RequestDelegate Next;
        public RequestDelegate Process;
        public Func<RequestDelegate, RequestDelegate> Component;
    }

    public interface ISimpleApplicationBuilder
    {
        void Use(Func<RequestDelegate, RequestDelegate> middleware);
        RequestDelegate Build();        
    }

    public class SimpleApplicationBuilder : ISimpleApplicationBuilder
    {
        public RequestDelegate Build()
        {
            var node = Components.Last;
            while(node != null)
            {
                node.Value.Next = GetNextFunc(node);
                node.Value.Process = node.Value.Component(node.Value.Next);
                node = node.Previous;
            }
            return Components.First.Value.Process;
        }

        private RequestDelegate GetNextFunc(LinkedListNode<MiddlewareComponentNode> node)
        {
            if(node.Next == null)
            {
                // no more middleware components left in the list 
                return ctx =>
                {
                    // consider a 404 since no other middleware processed the request
                    // ctx.Response.StatusCode = 404;
                    return Task.CompletedTask;
                };
            }
            else
            {
                return node.Next.Value.Process;
            }
        }

        public void Use(Func<RequestDelegate, RequestDelegate> component)
        {
            var node = new MiddlewareComponentNode
            {
                Component = component
            };

            Components.AddLast(node);
        }

        LinkedList<MiddlewareComponentNode> Components = new LinkedList<MiddlewareComponentNode>();
    }
}
