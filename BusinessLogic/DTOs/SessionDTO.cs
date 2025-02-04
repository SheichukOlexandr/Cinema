using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class SessionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ціна фільму обов'язкова")]
        public int MoviePriceId { get; set; }

        [Required(ErrorMessage = "Зал обов'язковий")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Дата сеансу обов'язкова")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Час сеансу обов'язковий")]
        public TimeOnly Time { get; set; }

        // data to map:
        public string? RoomName { get; set; }

        public decimal Price { get; set; }
        public int MovieId { get; set; }
    }
}
