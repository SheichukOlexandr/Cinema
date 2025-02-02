using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class MoviePriceDTO
    {
        [Range(0, double.MaxValue, ErrorMessage = "Ціна не може бути від'ємною.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Фільм є обов'язковим.")]
        public int MovieId { get; set; }
    }
}
