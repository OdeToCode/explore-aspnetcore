using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;

namespace Tests
{
    public class EmptyServer : IServer
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Start<TContext>(IHttpApplication<TContext> application)
        {
            throw new System.NotImplementedException();
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public IFeatureCollection Features { get; }
    }
}