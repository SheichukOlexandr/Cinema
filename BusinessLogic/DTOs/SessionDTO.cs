using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace BusinessLogic.DTOs
{
    public class SessionDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Ціна на фільм є обов'язковою.")]
        public int MoviePriceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Зал є обов'язковим.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Дата сеансу є обов'язковою.")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Час сеансу є обов'язковим.")]
        public TimeOnly Time { get; set; }

        public void Validate(Movie movie)
        {
            if (Date < movie.ReleaseDate)
            {
                throw new ValidationException("Сеанс не може відбутися до релізу фільму.");
            }
        }
    }
}
