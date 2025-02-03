using BusinessLogic.DTOs;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC_Cinema_app.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class GetMoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GetMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await _context.Movies
                .Include(m => m.Genre) // Підключаємо жанр
                .ToListAsync();

            var movieDtos = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Duration = m.Duration,
                Cast = m.Cast,
                GenreId = m.GenreId,
                GenreName = m.Genre?.Name ?? "Невідомий жанр", // Додаємо назву жанру
                ReleaseDate = m.ReleaseDate,
                Description = m.Description,
                MinAge = m.MinAge,
                Rating = m.Rating,
                StatusId = m.StatusId,
                PosterURL = m.PosterURL,
                TrailerURL = m.TrailerURL
            }).ToList();

            return Ok(movieDtos);
        }
    }
}
