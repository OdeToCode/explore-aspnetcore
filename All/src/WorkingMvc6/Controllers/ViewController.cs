using Microsoft.AspNet.Mvc;

namespace WorkingMvc6.Controllers
{
    [Route("[controller]/[action]")]
    public class ViewController : Controller
    {
        public ViewResult Injects()
        {
            return View();
        }

        public ViewResult Inherits()
        {
            return View();
        }
    }
}
