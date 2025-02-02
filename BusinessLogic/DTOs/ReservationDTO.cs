using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace BusinessLogic.DTOs
{
    public class ReservationDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Користувач є обов'язковим.")]
        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Сеанс є обов'язковим.")]
        public int SessionId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Місце є обов'язковим.")]
        public int SeatId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Статус бронювання є обов'язковим.")]
        public int StatusId { get; set; }

        public void Validate(Room room, int reservationsCount)
        {
            if (reservationsCount >= room.Capacity)
            {
                throw new ValidationException("Кількість бронювань перевищує місткість залу.");
            }
        }
    }
}
