using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;

namespace Controllers.Controllers
{
    [Route("[controller]")]
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            var headers = Request.GetTypedHeaders();
            return new ObjectResult(headers);
        }
    }
}
