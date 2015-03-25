using Microsoft.AspNet.Mvc;

namespace EmptyStart.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello");
        }
    }
}