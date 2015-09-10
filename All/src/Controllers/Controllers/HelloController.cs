using Controllers.Models;
using Microsoft.AspNet.Mvc;

namespace Controllers.Controllers
{
    [Route("/"), Route("[controller]")]
    public class HelloController : Controller
    {                   
        [HttpGet]  
        [ResponseCache(Duration=60)]  
        public Movie Get(string name, int id)
        {
            var result = new Movie() {Id = 1, Title = "Star Wars", Length = 90};
            return result;
        }
    }
}