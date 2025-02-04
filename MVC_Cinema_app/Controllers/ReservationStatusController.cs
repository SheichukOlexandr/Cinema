using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.DTOs;

namespace MVC_Cinema_app.Controllers
{
    public class ReservationStatusController : Controller
    {
        private readonly ReservationStatusService _reservationStatusService;

        public ReservationStatusController(ReservationStatusService reservationStatusService)
        {
            _reservationStatusService = reservationStatusService;
        }

        // GET: ReservationStatus
        public async Task<IActionResult> Index()
        {
            return View(await _reservationStatusService.GetAllAsync());
        }

        // GET: ReservationStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationStatus = await _reservationStatusService.GetAsync(id.Value);
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
        public async Task<IActionResult> Create([Bind("Id,Name")] ReservationStatusDTO reservationStatus)
        {
            if (ModelState.IsValid)
            {
                await _reservationStatusService.AddAsync(reservationStatus);
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

            var reservationStatus = await _reservationStatusService.GetAsync(id.Value);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ReservationStatusDTO reservationStatus)
        {
            if (id != reservationStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reservationStatusService.UpdateAsync(reservationStatus);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReservationStatusExists(reservationStatus.Id))
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

            var reservationStatus = await _reservationStatusService.GetAsync(id.Value);
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
            await _reservationStatusService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReservationStatusExists(int id)
        {
            return await _reservationStatusService.GetAsync(id) != null;
        }
    }
}
