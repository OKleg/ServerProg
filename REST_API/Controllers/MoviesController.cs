using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        MoviesContext db =  new MoviesContext();
      
        public MoviesController() {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Movies.Load();
        }   

        // GET: api/<MoviesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
                return await db.Movies.ToListAsync();
        }

        // GET api/<MoviesController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await db.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();
            return movie;
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            db.Movies.Add(movie);
            await db.SaveChangesAsync();
            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
                return BadRequest();
            db.Entry(movie).State = EntityState.Modified;
            try {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!MovieExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE api/<MoviesController>/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await db.Movies.FindAsync(id);
            if (movie == null)   
                return NotFound();  
            db.Movies.Remove(movie); 
            await db.SaveChangesAsync(); 
            return movie;
        }

        private bool MovieExists(int id) =>
            db.Movies.Any(e => e.MovieId == id);
    }
}
