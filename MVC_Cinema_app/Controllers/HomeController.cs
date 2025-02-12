using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Cinema_app.Models;
using System.Diagnostics;

namespace MVC_Cinema_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieService _movieService;
        private readonly GenreService _genreService;
        private readonly SessionService _sessionService;

        public HomeController(ILogger<HomeController> logger, MovieService movieService, GenreService genreService, SessionService sessionService)
        {
            _logger = logger;
            _movieService = movieService;
            _genreService = genreService;
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index()
        {
            var moviesWithSessions = await _sessionService.GetSessionsGroupedByMovies();

            var model = new GenreViewModel
            {
                GenreOptions = (await _genreService.GetAllAsync())
                    .Select(it => new SelectListItem
                    {
                        Value = it.Name,
                        Text = it.Name
                    }).ToList(),

                Movies = moviesWithSessions
                    .Select(it => new MovieViewModel
                    {
                        Movie = it.Movie,
                        Sessions = it.Sessions
                    }).ToList(),
            };

            return View(model);
        }

        public IActionResult Movies()
        {
            return View();
        }
        public IActionResult Sessions()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
