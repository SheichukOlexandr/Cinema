using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MVC_Cinema_app.Controllers
{
    [Authorize(Policy = Policies.AdminUserPolicy)]
    public class SeatsController : Controller
    {
        private readonly SeatService _seatService;

        public SeatsController(SeatService seatService)
        {
            _seatService = seatService;
        }

        // GET: Seats
        public async Task<IActionResult> Index()
        {
            return View(await _seatService.GetAllAsync());
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _seatService.GetAsync(id.Value);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // GET: Seats/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RoomId"] = new SelectList(await _seatService.GetAllRoomsAsync(), "Id", "Name");
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,Number,ExtraPrice")] SeatDTO seat)
        {
            if (ModelState.IsValid)
            {
                await _seatService.AddAsync(seat);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(await _seatService.GetAllRoomsAsync(), "Id", "Name", seat.RoomId);
            return View(seat);
        }

        // GET: Seats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _seatService.GetAsync(id.Value);
            if (seat == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(await _seatService.GetAllRoomsAsync(), "Id", "Name", seat.RoomId);
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,Number,ExtraPrice")] SeatDTO seat)
        {
            if (id != seat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _seatService.UpdateAsync(seat);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SeatExists(seat.Id))
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
            ViewData["RoomId"] = new SelectList(await _seatService.GetAllRoomsAsync(), "Id", "Name", seat.RoomId);
            return View(seat);
        }

        // GET: Seats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _seatService.GetAsync(id.Value);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _seatService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SeatExists(int id)
        {
            return await _seatService.GetAsync(id) != null;
        }
    }
}
