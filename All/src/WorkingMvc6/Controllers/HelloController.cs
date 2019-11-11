using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WorkingMvc6.Controllers
{        
    [Route("[controller]")]
    public class HelloController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public HelloController(IWebHostEnvironment env)
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


