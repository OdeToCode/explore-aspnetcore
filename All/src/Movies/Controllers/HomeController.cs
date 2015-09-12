using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Movies.Services;

namespace Movies.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        readonly MovieDb _db;

        public HomeController(MovieDb db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = await _db.Movies.ToListAsync();
            return View(movies);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);
            return View(movie);
        }

        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie != null)
            {
                _db.Movies.Remove(movie);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
