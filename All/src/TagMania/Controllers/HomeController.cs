using Microsoft.AspNet.Mvc;

namespace TagMania.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return Content("About");
        }
    }
}
