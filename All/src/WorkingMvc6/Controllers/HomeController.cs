using Microsoft.AspNetCore.Mvc;
using WorkingMvc6.Services;

namespace WorkingMvc6.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public HomeController(IGreeter greeter)
        {
            
        }

        public IActionResult Index([FromServices]IGreeter greeter)
        { 
            return View();
        }
    }
}
