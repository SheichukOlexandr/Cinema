using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Models;

namespace MVC_Cinema_app.Controllers
{
    public class MovieStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieStatuses.ToListAsync());
        }

        // GET: MovieStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieStatus = await _context.MovieStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Name")] MovieStatus movieStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieStatus);
                await _context.SaveChangesAsync();
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

            var movieStatus = await _context.MovieStatuses.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MovieStatus movieStatus)
        {
            if (id != movieStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieStatusExists(movieStatus.Id))
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

            var movieStatus = await _context.MovieStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var movieStatus = await _context.MovieStatuses.FindAsync(id);
            if (movieStatus != null)
            {
                _context.MovieStatuses.Remove(movieStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieStatusExists(int id)
        {
            return _context.MovieStatuses.Any(e => e.Id == id);
        }
    }
}
