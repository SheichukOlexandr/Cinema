using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Models;
using BusinessLogic.DTOs;

namespace MVC_Cinema_app.Controllers
{
    public class AdminMovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminMovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminMovie
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.Include(m => m.Genre).Include(m => m.Status).ToListAsync();
            var movieDtos = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Duration = m.Duration,
                Cast = m.Cast,
                GenreId = m.GenreId,
                ReleaseDate = m.ReleaseDate,
                Description = m.Description,
                MinAge = m.MinAge,
                Rating = m.Rating,
                StatusId = m.StatusId,
                PosterURL = m.PosterURL,
                TrailerURL = m.TrailerURL
            }).ToList();

            return View(movieDtos);
        }

        // GET: AdminMovie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Duration = movie.Duration,
                Cast = movie.Cast,
                GenreId = movie.GenreId,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                MinAge = movie.MinAge,
                Rating = movie.Rating,
                StatusId = movie.StatusId,
                PosterURL = movie.PosterURL,
                TrailerURL = movie.TrailerURL
            };

            return View(movieDto);
        }

        // GET: AdminMovie/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.MovieStatuses, "Id", "Name");
            return View();
        }

        // POST: AdminMovie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,Duration,Cast,GenreId,ReleaseDate,Description,MinAge,Rating,StatusId,PosterURL,TrailerURL")] MovieDTO movieDto)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Title = movieDto.Title,
                    Director = movieDto.Director,
                    Duration = movieDto.Duration,
                    Cast = movieDto.Cast,
                    GenreId = movieDto.GenreId,
                    ReleaseDate = movieDto.ReleaseDate,
                    Description = movieDto.Description,
                    MinAge = movieDto.MinAge,
                    Rating = movieDto.Rating,
                    StatusId = movieDto.StatusId,
                    PosterURL = movieDto.PosterURL,
                    TrailerURL = movieDto.TrailerURL
                };

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movieDto.GenreId);
            ViewData["StatusId"] = new SelectList(_context.MovieStatuses, "Id", "Name", movieDto.StatusId);
            return View(movieDto);
        }

        // GET: AdminMovie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Duration = movie.Duration,
                Cast = movie.Cast,
                GenreId = movie.GenreId,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                MinAge = movie.MinAge,
                Rating = movie.Rating,
                StatusId = movie.StatusId,
                PosterURL = movie.PosterURL,
                TrailerURL = movie.TrailerURL
            };

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movieDto.GenreId);
            ViewData["StatusId"] = new SelectList(_context.MovieStatuses, "Id", "Name", movieDto.StatusId);
            return View(movieDto);
        }

        // POST: AdminMovie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Duration,Cast,GenreId,ReleaseDate,Description,MinAge,Rating,StatusId,PosterURL,TrailerURL")] MovieDTO movieDto)
        {
            if (id != movieDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var movie = await _context.Movies.FindAsync(id);
                    if (movie == null)
                    {
                        return NotFound();
                    }

                    movie.Title = movieDto.Title;
                    movie.Director = movieDto.Director;
                    movie.Duration = movieDto.Duration;
                    movie.Cast = movieDto.Cast;
                    movie.GenreId = movieDto.GenreId;
                    movie.ReleaseDate = movieDto.ReleaseDate;
                    movie.Description = movieDto.Description;
                    movie.MinAge = movieDto.MinAge;
                    movie.Rating = movieDto.Rating;
                    movie.StatusId = movieDto.StatusId;
                    movie.PosterURL = movieDto.PosterURL;
                    movie.TrailerURL = movieDto.TrailerURL;

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movieDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movieDto.GenreId);
            ViewData["StatusId"] = new SelectList(_context.MovieStatuses, "Id", "Name", movieDto.StatusId);
            return View(movieDto);
        }

        // GET: AdminMovie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Duration = movie.Duration,
                Cast = movie.Cast,
                GenreId = movie.GenreId,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                MinAge = movie.MinAge,
                Rating = movie.Rating,
                StatusId = movie.StatusId,
                PosterURL = movie.PosterURL,
                TrailerURL = movie.TrailerURL
            };

            return View(movieDto);
        }

        // POST: AdminMovie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
