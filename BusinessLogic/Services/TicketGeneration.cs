using BusinessLogic.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
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
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Header()
                        .Text("🎟️ Квиток на фільм")
                        .SemiBold().FontSize(24).AlignCenter();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(10);
                            x.Item().Text($"👤 Глядач: {reservation.UserFullName}");
                            x.Item().Text($"🎬 Фільм: {reservation.Session.MovieName}");
                            x.Item().Text($"📅 Дата: {reservation.Session.Date:dd-MM-yyyy}");
                            x.Item().Text($"🕒 Час: {reservation.Session.Time:hh\\:mm}");
                            x.Item().Text($"🏢 Зал: {reservation.Session.RoomName}");
                            x.Item().Text($"💺 Місце: {reservation.SeatNumber}");
                            x.Item().Text($"🎟️ Ціна сеансу: {reservation.Session.Price} грн");
                            x.Item().Text($"💰 Ціна місця: {reservation.SeatExtraPrice} грн");
                            x.Item().Text($"💳 Загальна сума: {reservation.Session.Price + reservation.SeatExtraPrice} грн");
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("🎥 Дякуємо за покупку! ");
                            x.Span("КіноМанія");
                        });
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
    }

}
