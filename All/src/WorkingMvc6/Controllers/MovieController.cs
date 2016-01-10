using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;


namespace WorkingMvc6.Controllers
{
    public class Movie
    {
        [Required]
        public string Title { get; set; }
    }

    [Route("[controller]/[action]")]
    public class MovieController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Movie { Title="Star Wars"});
        }

        [HttpPost]
        public IActionResult Update(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", movie);
            }
            return new ObjectResult(movie);
        }

        [HttpGet, HttpPost]      
        public IActionResult Search(string term)
        {
            return Content(term);
        }

        [HttpPut]
        public IActionResult UpdateAjax([FromBody] Movie movie)
        {            
            return new ObjectResult(movie);
        }
    }
}
