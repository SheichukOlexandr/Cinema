using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.DTOs;

namespace MVC_Cinema_app.Controllers
{
    public class MovieStatusController : Controller
    {
        private readonly MovieStatusService _movieStatusService;

        public MovieStatusController(MovieStatusService movieStatusService)
        {
            _movieStatusService = movieStatusService;
        }

        // GET: MovieStatus
        public async Task<IActionResult> Index()
        {
            return View(await _movieStatusService.GetAllAsync());
        }

        // GET: MovieStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieStatus = await _movieStatusService.GetAsync(id.Value);
            if (movieStatus == null)
            {
                return NotFound();
            }

            return View(movieStatus);
        }

        // GET: MovieStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MovieStatusDTO movieStatus)
        {
            if (ModelState.IsValid)
            {
                await _movieStatusService.AddAsync(movieStatus);
                return RedirectToAction(nameof(Index));
            }
            return View(movieStatus);
        }

        // GET: MovieStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieStatus = await _movieStatusService.GetAsync(id.Value);
            if (movieStatus == null)
            {
                return NotFound();
            }
            return View(movieStatus);
        }

        // POST: MovieStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MovieStatusDTO movieStatus)
        {
            if (id != movieStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieStatusService.UpdateAsync(movieStatus);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MovieStatusExistsAsync(movieStatus.Id))
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
            return View(movieStatus);
        }

        // GET: MovieStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieStatus = await _movieStatusService.GetAsync(id.Value);
            if (movieStatus == null)
            {
                return NotFound();
            }

            return View(movieStatus);
        }

        // POST: MovieStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieStatusService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MovieStatusExistsAsync(int id)
        {
            return await _movieStatusService.GetAsync(id) != null;
        }
    }
}
