using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace BusinessLogic.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Користувач обов'язковий")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Сеанс обов'язковий")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Місце обов'язкове")]
        public int SeatId { get; set; }

        [Required(ErrorMessage = "Статус бронювання обов'язковий")]
        public int StatusId { get; set; }

        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
        public virtual Seat Seat { get; set; }

        // data to map:
        public string? UserFullName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? MovieName { get; set; }
        public int SeatNumber { get; set; }
        public string? RoomName { get; set; }
        public string? StatusName { get; set; }
    }
}
