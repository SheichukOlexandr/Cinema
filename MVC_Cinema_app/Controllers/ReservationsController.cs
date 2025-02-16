using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC_Cinema_app.Controllers
{
    [Authorize(Policy = Policies.AdminUserPolicy)]
    public class ReservationsController : Controller
    {
        private readonly ReservationService _reservationService;

        public ReservationsController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _reservationService.GetAllAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SeatId"] = new SelectList(await _reservationService.GetAllSeatsAsync(), "Id", "SeatName");
            ViewData["SessionId"] = new SelectList(await _reservationService.GetAllSessionsAsync(), "Id", "Id");
            ViewData["StatusId"] = new SelectList(await _reservationService.GetAllReservationStatusesAsync(), "Id", "Name");
            ViewData["UserId"] = new SelectList(await _reservationService.GetAllUsersAsync(), "Id", "Email");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,SessionId,SeatId,StatusId")] ReservationDTO reservation)
        {
            if (ModelState.IsValid)
            {
                await _reservationService.AddAsync(reservation);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeatId"] = new SelectList(await _reservationService.GetAllSeatsAsync(), "Id", "SeatName");
            ViewData["SessionId"] = new SelectList(await _reservationService.GetAllSessionsAsync(), "Id", "Id");
            ViewData["StatusId"] = new SelectList(await _reservationService.GetAllReservationStatusesAsync(), "Id", "Name");
            ViewData["UserId"] = new SelectList(await _reservationService.GetAllUsersAsync(), "Id", "Email");
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["SeatId"] = new SelectList(await _reservationService.GetAllSeatsAsync(), "Id", "SeatName");
            ViewData["SessionId"] = new SelectList(await _reservationService.GetAllSessionsAsync(), "Id", "Id");
            ViewData["StatusId"] = new SelectList(await _reservationService.GetAllReservationStatusesAsync(), "Id", "Name");
            ViewData["UserId"] = new SelectList(await _reservationService.GetAllUsersAsync(), "Id", "Email");
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SessionId,SeatId,StatusId")] ReservationDTO reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reservationService.UpdateAsync(reservation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReservationExists(reservation.Id))
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
            ViewData["SeatId"] = new SelectList(await _reservationService.GetAllSeatsAsync(), "Id", "SeatName", reservation.SeatId);
            ViewData["SessionId"] = new SelectList(await _reservationService.GetAllSessionsAsync(), "Id", "Id", reservation.SessionId);
            ViewData["StatusId"] = new SelectList(await _reservationService.GetAllReservationStatusesAsync(), "Id", "Name", reservation.StatusId);
            ViewData["UserId"] = new SelectList(await _reservationService.GetAllUsersAsync(), "Id", "Email", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReservationExists(int id)
        {
            return await _reservationService.GetAsync(id) != null;
        }
    }
}
