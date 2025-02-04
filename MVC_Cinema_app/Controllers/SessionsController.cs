using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC_Cinema_app.Controllers
{
    public class SessionsController : Controller
    {
        private readonly SessionsService _sessionService;

        public SessionsController(SessionsService sessionService)
        {
            _sessionService = sessionService;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            return View(await _sessionService.GetAllAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _sessionService.GetAsync(id.Value);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MovieId"] = new SelectList(await _sessionService.GetAllMoviesAsync(), "Id", "Title");
            ViewData["RoomId"] = new SelectList(await _sessionService.GetAllRoomsAsync(), "Id", "Name");
            return View();
        }

        // POST: Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,MoviePriceId,RoomId,Date,Time")] SessionDTO session)
        {
            if (ModelState.IsValid)
            {
                await _sessionService.AddAsync(session);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(await _sessionService.GetAllMoviesAsync(), "Id", "Title");
            ViewData["RoomId"] = new SelectList(await _sessionService.GetAllRoomsAsync(), "Id", "Name");
            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _sessionService.GetAsync(id.Value);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["MoviePriceId"] = new SelectList(await _sessionService.GetAllMoviePricesAsync(), "Id", "MoviePriceName", session.MoviePriceId);
            ViewData["RoomId"] = new SelectList(await _sessionService.GetAllRoomsAsync(), "Id", "Name", session.RoomId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,MoviePriceId,RoomId,Date,Time")] SessionDTO session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _sessionService.UpdateAsync(session);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SessionExists(session.Id))
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
            ViewData["MoviePriceId"] = new SelectList(await _sessionService.GetAllMoviePricesAsync(), "Id", "MoviePriceName", session.MoviePriceId);
            ViewData["RoomId"] = new SelectList(await _sessionService.GetAllRoomsAsync(), "Id", "Name", session.RoomId);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _sessionService.GetAsync(id.Value);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sessionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SessionExists(int id)
        {
            return await _sessionService.GetAsync(id) != null;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrices(int movieId)
        {
            var prices = await _sessionService.GetPricesByMovieIdAsync(movieId);
            return Json(prices);
        }
    }
}
