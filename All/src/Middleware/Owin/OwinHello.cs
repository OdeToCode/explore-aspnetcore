using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;


    public class OwinHello
    {
        private AppFunc next;

        public OwinHello(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var responseStream = (Stream)environment["owin.ResponseBody"];
            var message = Encoding.UTF8.GetBytes("Hello, from Owin!");
            await responseStream.WriteAsync(message, 0, message.Length);
        }

        public static void Create(Action<Func<AppFunc, AppFunc>> pipeline)
        {
            pipeline(next =>      
            {
                var hello = new OwinHello(next);
                return hello.Invoke;
            });
        }
    }
}
