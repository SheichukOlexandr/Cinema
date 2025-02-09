using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MVC_Cinema_app.Controllers
{
    [Authorize(Policy = Policies.AdminUserPolicy)]
    public class MoviePricesController : Controller
    {
        private readonly MoviePriceService _moviePriceService;

        public MoviePricesController(MoviePriceService moviePriceService)
        {
            _moviePriceService = moviePriceService;
        }

        // GET: MoviePrices
        public async Task<IActionResult> Index()
        {
            return View(await _moviePriceService.GetAllAsync());
        }

        // GET: MoviePrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePrice = await _moviePriceService.GetAsync(id.Value);
            if (moviePrice == null)
            {
                return NotFound();
            }

            return View(moviePrice);
        }

        // GET: MoviePrices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MovieId"] = new SelectList(await _moviePriceService.GetAllMoviesAsync(), "Id", "Title");
            return View();
        }

        // POST: MoviePrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,Price")] MoviePriceDTO moviePriceDTO)
        {
            if (ModelState.IsValid)
            {
                await _moviePriceService.AddAsync(moviePriceDTO);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(await _moviePriceService.GetAllMoviesAsync(), "Id", "Title", moviePriceDTO.MovieId);
            return View(moviePriceDTO);
        }

        // GET: MoviePrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePrice = await _moviePriceService.GetAsync(id.Value);
            if (moviePrice == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(await _moviePriceService.GetAllMoviesAsync(), "Id", "Title", moviePrice.MovieId);
            return View(moviePrice);
        }

        // POST: MoviePrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,Price")] MoviePriceDTO moviePriceDTO)
        {
            if (id != moviePriceDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _moviePriceService.UpdateAsync(moviePriceDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MoviePriceExists(moviePriceDTO.Id))
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
            ViewData["MovieId"] = new SelectList(await _moviePriceService.GetAllMoviesAsync(), "Id", "Title", moviePriceDTO.MovieId);
            return View(moviePriceDTO);
        }

        // GET: MoviePrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePrice = await _moviePriceService.GetAsync(id.Value);
            if (moviePrice == null)
            {
                return NotFound();
            }

            return View(moviePrice);
        }

        // POST: MoviePrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _moviePriceService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MoviePriceExists(int id)
        {
            return await _moviePriceService.GetAsync(id) != null;
        }
    }
}
