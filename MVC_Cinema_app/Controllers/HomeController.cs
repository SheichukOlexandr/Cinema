using BusinessLogic.DTOs;
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
        private readonly ReservationService _reservationService;
        private readonly GenreService _genreService;
        private readonly SessionService _sessionService;
        private readonly UserService _userService;
        private readonly ReservationStatusService _reservationStatusService;

        public HomeController(
            ILogger<HomeController> logger, 
            ReservationService reservationService, 
            GenreService genreService, 
            SessionService sessionService,
            UserService userService,
            ReservationStatusService reservationStatusService)
        {
            _logger = logger;
            _reservationService = reservationService;
            _genreService = genreService;
            _sessionService = sessionService;
            _userService = userService;
            _reservationStatusService = reservationStatusService;
        }

        public async Task<IActionResult> Index()
        {
            var allGenres = await _genreService.GetAllAsync();
            var genreOptions = allGenres.Select(it => new SelectListItem { Value = it.Name, Text = it.Name }).ToList();

            var moviesWithSessions = await _sessionService.GetSessionsGroupedByMovies();

            var movieViews = moviesWithSessions.Select(it => new MovieDetailsViewModel
            {
                Movie = it.Movie,
                SessionDetails = it.Sessions.Select(session => new SessionDetailsViewModel
                {
                    Session = session,
                    AvailableSeats = _sessionService.GetAvailableSeatsInSessionAsync(session).Result.Select(seat => new SelectListItem
                    {
                        Value = seat.Id.ToString(),
                        Text = seat.SeatName + " – " + seat.ExtraPrice
                    })
                })
            }).ToList();

            var newMovies = moviesWithSessions.Select(it => it.Movie).Where(it => DateTime.Now.Year - it.ReleaseDate.Year < 2).ToList();

            var model = new HomeViewModel { 
                GenreOptions = genreOptions, 
                Movies = movieViews,
                NewMovies = newMovies
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

        public async Task<IActionResult> Details(int id)
        {
            var moviesWithSessions = await _sessionService.GetSessionsGroupedByMovies(sessionFilter: it => it.Movie.Id == id);
            var data = moviesWithSessions.First();

            var model = new MovieDetailsViewModel
            {
                Movie = data.Movie,
                SessionDetails = data.Sessions.Select(session => new SessionDetailsViewModel
                {
                    Session = session,
                    AvailableSeats = _sessionService.GetAvailableSeatsInSessionAsync(session).Result.Select(seat => new SelectListItem
                    {
                        Value = seat.Id.ToString(),
                        Text = seat.SeatName + " – " + seat.ExtraPrice
                    })
                })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int id, int? seatId, int movieId)
        {
            var user = await _userService.GetCurrentUserAsync(this.User);
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!seatId.HasValue || seatId == 0)
            {
                return RedirectToAction("Details", new { id = movieId });
            }

            var session = await _sessionService.GetAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            var status = (await _reservationStatusService.GetAllAsync()).FirstOrDefault(it => it.Name == ReservationStatusDTO.Created);
            if (status == null)
            {
                return NotFound();
            }


            var reservation = new ReservationDTO
            {
                UserId = user.Id,
                SessionId = session.Id,
                SeatId = seatId.Value,
                StatusId = status.Id
            };

            await _reservationService.AddAsync(reservation);

            return RedirectToAction(nameof(Index));
        }
    }
}
