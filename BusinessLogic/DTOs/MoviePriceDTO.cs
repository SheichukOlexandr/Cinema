using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class MoviePriceDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Фільм обов'язковий")]
        public int MovieId { get; set; }

        [Range(0, 1000, ErrorMessage = "Ціна повинна бути від 0 до 1000")]
        public decimal Price { get; set; }

        // data to map:
        public string? MovieName { get; set; }
        public string? MoviePriceName { get; set; }
    }
}
