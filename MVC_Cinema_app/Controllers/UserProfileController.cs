using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using System.Security.Claims;
using MVC_Cinema_app.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVC_Cinema_app.Controllers
{
    [Authorize(Policy = Policies.DefaultUserPolicy)]
    public class UserProfileController : Controller
    {
        private readonly UserService _userService;
        private readonly ReservationService _reservationService;

        public UserProfileController(UserService userService, ReservationService reservationService)
        {
            _userService = userService;
            _reservationService = reservationService;
        }

        // GET: UserProfile
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetCurrentUserAsync(this.User);
            if (user == null)
            {
                return NotFound();
            }

            var reservations = await _reservationService.GetAllReservationsByUserIdAsync(user.Id);
            var model = new UserProfileViewModel { 
                User = user,
                Reservations = reservations
            };

            return View(model);
        }

        // GET: UserProfile/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await _userService.GetCurrentUserAsync(this.User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserEditViewModel
            {
                User = user
            };

            return View(model);
        }

        // POST: UserProfile/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("User, NewPasswordConfirmed")] UserEditViewModel model)
        {
            if (model.NewPasswordConfirmed != model.User.NewPassword)
            {
                ModelState.AddModelError("User.NewPassword", "Паролі не співпадають");
                ModelState.AddModelError("NewPasswordConfirmed", "Паролі не співпадають");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateAsync(model.User);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(model.User.Id))
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
            return View(model);
        }

        // POST: UserProfile/Cancel/3
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int reservationId)
        {
            var user = await _userService.GetCurrentUserAsync(this.User);
            if (user == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetAsync(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationService.CancelReservationAsync(reservation);

            return RedirectToAction(nameof(Index));
        }

        // POST: UserProfile/Confirm/3
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int reservationId)
        {
            var user = await _userService.GetCurrentUserAsync(this.User);
            if (user == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetAsync(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationService.ConfirmReservationAsync(reservation);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(int id)
        {
            return await _userService.GetAsync(id) != null;
        }
    }
}
