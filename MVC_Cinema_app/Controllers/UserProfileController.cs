﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly TicketGeneration _ticketGeneration; // Сервіс генерації квитків
        private readonly EmailService _emailService; // Сервіс відправки email

        public UserProfileController(
            UserService userService,
            ReservationService reservationService,
            TicketGeneration ticketGeneration,
            EmailService emailService)
        {
            _userService = userService;
            _reservationService = reservationService;
            _ticketGeneration = ticketGeneration; // Ініціалізація сервісу квитків
            _emailService = emailService; // Ініціалізація сервісу email
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
            var model = new UserProfileViewModel
            {
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

        // POST: UserProfile/Cancel/{reservationId}
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

        // POST: UserProfile/Confirm/{reservationId}
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

        // GET: UserProfile/GenerateTicket/{reservationId}
        [HttpGet]
        public async Task<IActionResult> GenerateTicket(int reservationId)
        {
            // Отримання даних бронювання
            var reservation = await _reservationService.GetAsync(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            // Генерація PDF-квитка через сервіс TicketGeneration
            var ticketBytes = _ticketGeneration.GenerateTicket(reservation);

            // Повернення PDF-файлу користувачеві
            return File(ticketBytes, "application/pdf", $"Ticket_{reservation.Session.Date}_{reservation.Session.Time}.pdf");
        }

        // POST: UserProfile/SendTicketToEmail/{reservationId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendTicketToEmail(int reservationId)
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

            if (reservation.StatusName != ReservationStatusDTO.Confirmed && reservation.StatusName != ReservationStatusDTO.Completed)
            {
                return BadRequest("Квиток можна відправити тільки для підтверджених або завершених бронювань.");
            }

            // Генерація PDF-квитка
            var ticketBytes = _ticketGeneration.GenerateTicket(reservation);

            // Текст листа
            string emailBody = $"Вітаємо, {user.FirstName}! Ваш квиток на фільм \"{reservation.Session.MovieName}\" додається у вкладенні.";

            // Відправка квитка на email
            bool emailSent = await _emailService.SendTicketEmailAsync(
                user.Email, // Електронна пошта користувача
                "Ваш електронний квиток", // Тема листа
                emailBody, // Текст листа
                ticketBytes, // PDF-квиток
                $"Квиток_{reservation.Session.Date}_{reservation.Session.Time}.pdf" // Назва файлу
            );

            if (!emailSent)
            {
                return StatusCode(500, "Не вдалося надіслати квиток. Спробуйте ще раз пізніше.");
            }

            TempData["SuccessMessage"] = "Квиток успішно надіслано на вашу пошту!";
            return RedirectToAction(nameof(Index));
        }

        // POST: UserProfile/Delete/{reservationId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int reservationId)
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

            if (reservation.StatusName != ReservationStatusDTO.Cancelled)
            {
                return BadRequest("Видаляти можна тільки скасовані бронювання.");
            }

            bool isDeleted = await _reservationService.DeleteReservationAsync(reservationId);
            if (!isDeleted)
            {
                return StatusCode(500, "Помилка при видаленні бронювання.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}