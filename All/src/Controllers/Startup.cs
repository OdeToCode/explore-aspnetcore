using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Xml;
using Microsoft.Framework.DependencyInjection;

namespace Controllers
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(o =>
            {
                o.InputFormatters.Add(new XmlSerializerInputFormatter());
                o.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(rb =>
            {            
            });
        }
    }
}
