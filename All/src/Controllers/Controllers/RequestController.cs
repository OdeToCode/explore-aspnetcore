using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    [Route("[controller]")]
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            var requestHeaders = Request.GetTypedHeaders();
            var responseHeaders = Response.GetTypedHeaders();

            var mediaType = requestHeaders.Accept[0].MediaType;
            long? length = responseHeaders.ContentLength;
        
            return View(requestHeaders);
        }
    }
}
