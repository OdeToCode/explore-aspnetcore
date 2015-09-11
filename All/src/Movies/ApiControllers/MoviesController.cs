using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Movies.Entities;
using Movies.Services;

namespace Movies.ApiControllers
{
    [Route("/api/[controller]")]
    public class MoviesController : Controller
    {
        readonly MovieDb _db;

        public MoviesController(MovieDb db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _db.Movies.ToListAsync();
            return new ObjectResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie newMovie)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _db.Movies.Add(newMovie);
            await _db.SaveChangesAsync();
            return CreatedAtAction("Get", new {id = newMovie.Id}, newMovie);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Movie updatedMovie)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            _db.Entry(updatedMovie).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return new ObjectResult(updatedMovie);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
            return new ObjectResult(movie);
        }

    }
}
