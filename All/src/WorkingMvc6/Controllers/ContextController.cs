using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WorkingMvc6.Controllers
{
    [Route("[controller]/[action]")]
    public class ContextController : Controller
    {
        public IActionResult Connection()
        { 
            return new ObjectResult(HttpContext.Connection);
        }

        public IActionResult Features()
        {
            var result = HttpContext.Features.Select(kv => kv.Key.Name);
            return new ObjectResult(result);
        }

        public IActionResult Items()
        {
            return new ObjectResult(HttpContext.Items);
        }
    }
}
