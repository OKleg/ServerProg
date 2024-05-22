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
        MoviesContext _context;
      
        public MoviesController(MoviesContext _db) {
            //.Database.EnsureCreated();
            // гарантируем, что база данных создана
            _context = _db;
            // загружаем данные из БД
            _context.Movies.Load();
        }   

        // GET: api/<MoviesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
                return Ok(await _context.Movies.ToListAsync());
        }

        // GET api/<MoviesController>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return Ok(CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie));
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
                return BadRequest();
            _context.Entry(movie).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!MovieExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok(NoContent());
        }

        // DELETE api/<MoviesController>/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)   
                return NotFound();  
            _context.Movies.Remove(movie); 
            await _context.SaveChangesAsync(); 
            return Ok(movie);
        }

        private bool MovieExists(int id) =>
            _context.Movies.Any(e => e.MovieId == id);
    }
}
