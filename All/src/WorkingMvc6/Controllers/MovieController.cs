using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Cors;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;


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
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "seconds";

        public MovieController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Movie { Title="Star Wars" });
        }

        [HttpGet]
        [EnableCors("MyCors")]
        [ResponseCache(CacheProfileName= "Aggressive")]
        public IActionResult Time()
        {
            var value = 0;
            if (!_cache.TryGetValue(_cacheKey, out value))
            {
                value = DateTime.Now.Second;
                _cache.Set(_cacheKey, value, new MemoryCacheEntryOptions
                { 
                  AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10)
                });
            }


            return View(value);
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
