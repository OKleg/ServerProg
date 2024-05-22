using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Models;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCrewsController : ControllerBase
    {
        private readonly MoviesContext _context;

        public MovieCrewsController(MoviesContext context)
        {
            _context = context;
        }

        // GET: api/MovieCrews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieCrew>>> GetMovieCrews()
        {
          if (_context.MovieCrews == null)
          {
              return NotFound();
          }
            return Ok(await _context.MovieCrews.ToListAsync());
        }

        // GET: api/MovieCrews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieCrew>> GetMovieCrew(long? id)
        {
          if (_context.MovieCrews == null)
          {
              return NotFound();
          }
            var movieCrew = await _context.MovieCrews.FindAsync(id);

            if (movieCrew == null)
            {
                return NotFound();
            }

            return Ok(movieCrew);
        }

        // PUT: api/MovieCrews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieCrew(long? id, MovieCrew movieCrew)
        {
            if (id != movieCrew.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movieCrew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieCrewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(NoContent());
        }

        // POST: api/MovieCrews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieCrew>> PostMovieCrew(MovieCrew movieCrew)
        {
          if (_context.MovieCrews == null)
          {
              return Problem("Entity set 'MoviesContext.MovieCrews'  is null.");
          }
            _context.MovieCrews.Add(movieCrew);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MovieCrewExists(movieCrew.MovieId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(CreatedAtAction("GetMovieCrew", new { id = movieCrew.MovieId }, movieCrew));
        }

        // DELETE: api/MovieCrews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieCrew(long? id)
        {
            if (_context.MovieCrews == null)
            {
                return NotFound();
            }
            var movieCrew = await _context.MovieCrews.FindAsync(id);
            if (movieCrew == null)
            {
                return NotFound();
            }

            _context.MovieCrews.Remove(movieCrew);
            await _context.Movies
                .Where(m => m.MovieId == movieCrew.MovieId)
                .ForEachAsync(m => _context.Movies.Remove(m));
               
            await _context.SaveChangesAsync();

            return Ok(NoContent());
        }

        private bool MovieCrewExists(long? id)
        {
            return (_context.MovieCrews?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
