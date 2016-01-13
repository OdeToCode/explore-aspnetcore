using System.Runtime.Serialization;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace WorkingMvc6.Controllers
{        
    [Route("[controller]")]
    public class HelloController : Controller
    {
        private readonly IApplicationEnvironment _env;

        public HelloController(IApplicationEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_env);
        }

        [HttpGet("[action]")]
        public IActionResult Services([FromServices] IServiceCollection services)
        {            
            return View(services);            
        }

    }
  
}


