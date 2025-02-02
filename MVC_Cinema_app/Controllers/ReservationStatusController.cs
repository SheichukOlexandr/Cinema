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
    public class ReservationStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReservationStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReservationStatuses.ToListAsync());
        }

        // GET: ReservationStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // GET: ReservationStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservationStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ReservationStatus reservationStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationStatus);
        }

        // GET: ReservationStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses.FindAsync(id);
            if (reservationStatus == null)
            {
                return NotFound();
            }
            return View(reservationStatus);
        }

        // POST: ReservationStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ReservationStatus reservationStatus)
        {
            if (id != reservationStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationStatusExists(reservationStatus.Id))
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
            return View(reservationStatus);
        }

        // GET: ReservationStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // POST: ReservationStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationStatus = await _context.ReservationStatuses.FindAsync(id);
            if (reservationStatus != null)
            {
                _context.ReservationStatuses.Remove(reservationStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationStatusExists(int id)
        {
            return _context.ReservationStatuses.Any(e => e.Id == id);
        }
    }
}
