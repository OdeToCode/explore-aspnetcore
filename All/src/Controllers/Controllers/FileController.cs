using Microsoft.AspNet.Mvc;

namespace Controllers.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return File()
        }
    }
}
