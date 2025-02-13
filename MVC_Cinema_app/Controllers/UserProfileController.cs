using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using System.Security.Claims;
using MVC_Cinema_app.Models;
using Microsoft.AspNetCore.Authorization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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

        private async Task<UserDTO?> GetCurrentUserAsync()
        {
            var emailClaim = this.User.Claims.FirstOrDefault(it => it.Type == ClaimTypes.Email);
            if (emailClaim == null)
            {
                return null;
            }

            var email = emailClaim.Value;
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        // GET: UserProfile
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
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
            var user = await GetCurrentUserAsync();
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
            var user = await GetCurrentUserAsync();
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
            var user = await GetCurrentUserAsync();
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
        private byte[] GenerateTicketInternal(string movieTitle, string date, string time, string hall, int seatNumber, decimal sessionPrice, decimal seatPrice)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5); // Розмір сторінки A5
                    page.Margin(2, Unit.Centimetre); // Відступи
                    page.PageColor(Colors.White); // Колір фону
                    page.DefaultTextStyle(x => x.FontSize(14)); // Розмір шрифту

                    // Заголовок квитка
                    page.Header()
                        .Text("Квиток на фільм")
                        .SemiBold().FontSize(24).AlignCenter();

                    // Основна інформація
                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(10); // Відступи між елементами

                            x.Item().Text($"Фільм: {movieTitle}");
                            x.Item().Text($"Дата: {date}");
                            x.Item().Text($"Час: {time}");
                            x.Item().Text($"Зал: {hall}");
                            x.Item().Text($"Місце: {seatNumber}");
                            x.Item().Text($"Ціна сеансу: {sessionPrice} грн");
                            x.Item().Text($"Ціна місця: {seatPrice} грн");
                            x.Item().Text($"Загальна сума: {sessionPrice + seatPrice} грн");
                        });

                    // Підвал квитка
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Дякуємо за покупку! ");
                            x.Span("КіноМанія");
                        });
                });
            });

            // Генерація PDF у вигляді масиву байтів
            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
        [HttpGet]
        public async Task<IActionResult> GenerateTicket(int reservationId)
        {
            // Отримання даних бронювання
            var reservation = await _reservationService.GetAsync(reservationId);

            if (reservation == null)
            {
                return NotFound(); // Якщо бронювання не знайдено
            }

            // Генерація PDF-квитка
            var ticketBytes = GenerateTicketInternal(
                 reservation.Session.MovieName,
                 reservation.Session.Date.ToString("dd-MM-yyyy"),
                 reservation.Session.Time.ToString(@"hh:mm"), // Формат часу
                 reservation.Session.RoomName,
                 reservation.SeatNumber,
                 reservation.Session.Price,
                 reservation.SeatExtraPrice
             );

            // Повернення PDF-файлу користувачеві
            return File(ticketBytes, "application/pdf", $"Ticket_{reservationId}.pdf");
        }
    }
}
