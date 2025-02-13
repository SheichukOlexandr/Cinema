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

        // data to map:
        public string? UserFullName { get; set; }
        public SessionDTO? Session { get; set; }
        public decimal SeatExtraPrice { get; set; }
        public string? StatusName { get; set; }
        public string MovieTitle { get; set; }        // Назва фільму
        public DateTime Date { get; set; }            // Дата сеансу
        public TimeSpan Time { get; set; }            // Час сеансу
        public string HallName { get; set; }          // Назва залу
        public int SeatNumber { get; set; }           // Номер місця
        public decimal SessionPrice { get; set; }     // Ціна сеансу
        public decimal SeatPrice { get; set; }        // Додаткова ціна місця
    }
}
