using BusinessLogic.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

namespace BusinessLogic.Services
{
    public class TicketGeneration
    {
        public byte[] GenerateTicket(ReservationDTO reservation)
        {
            return GenerateTicketInternal(reservation);
        }

        private byte[] GenerateTicketInternal(ReservationDTO reservation)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.Grey.Darken3); // ✅ Темно-сірий фон для контрасту
                    page.DefaultTextStyle(x => x.FontSize(14).FontColor(Colors.White));

                    // 🔹 Заголовок квитка
                    page.Header()
                        .AlignCenter()
                        .Text("🎟️ Квиток у кіно")
                        .SemiBold().FontSize(28).FontColor(Colors.Yellow.Accent3); // ✅ Яскравий заголовок

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(12);

                            // 🔹 Декоративна смуга
                            x.Item().BorderBottom(2).BorderColor(Colors.Yellow.Lighten2);

                            x.Item().Text($"👤 Глядач: {reservation.UserFullName}").Bold();
                            x.Item().Text($"🎬 Фільм: {reservation.Session.MovieName}").Bold().FontColor(Colors.Orange.Accent3);
                            x.Item().Text($"📅 Дата: {reservation.Session.Date:dd-MM-yyyy}");
                            x.Item().Text($"🕒 Час: {reservation.Session.Time:hh\\:mm}");
                            x.Item().Text($"🏢 Зал: {reservation.Session.RoomName}");
                            x.Item().Text($"💺 Місце: {reservation.SeatNumber}");
                            x.Item().Text($"🎟️ Ціна сеансу: {reservation.Session.Price} грн");
                            x.Item().Text($"💰 Ціна місця: {reservation.SeatExtraPrice} грн");
                            x.Item().Text($"💳 Загальна сума: {reservation.Session.Price + reservation.SeatExtraPrice} грн")
                                .SemiBold().FontSize(16).FontColor(Colors.Green.Accent3);

                            x.Item().Text($"📌 Статус бронювання: {reservation.StatusName}").Bold();
                            x.Item().Text($"🕓 Дата і час генерації: {DateTime.Now:dd-MM-yyyy HH:mm}");

                            // 🔹 Нижня декоративна смуга
                            x.Item().PaddingVertical(10).BorderTop(2).BorderColor(Colors.Yellow.Lighten2);
                        });

                    // 🔹 Футер
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("🎥 Дякуємо за покупку! ").FontSize(16);
                            x.Span("КіноМанія").SemiBold().FontSize(18).FontColor(Colors.Blue.Accent3);
                        });
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
    }
}
