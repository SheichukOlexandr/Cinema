using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        [StringLength(100, ErrorMessage = "Назва не може бути довшою за 100 символів")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Режисер обов'язковий")]
        [StringLength(100, ErrorMessage = "Ім'я режисера не може бути довшим за 100 символів")]
        public string Director { get; set; }

        [Range(1, 500, ErrorMessage = "Тривалість фільму повинна бути від 1 до 500 хвилин")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Акторський склад обов'язковий")]
        [StringLength(300, ErrorMessage = "Список акторів не може бути довшим за 300 символів")]
        public string Cast { get; set; }

        [Required(ErrorMessage = "Жанр обов'язковий")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Дата виходу обов'язкова")]
        public DateOnly ReleaseDate { get; set; }

        [StringLength(500, ErrorMessage = "Опис не може бути довшим за 500 символів")]
        public string Description { get; set; }

        [Range(0, 21, ErrorMessage = "Мінімальний вік повинен бути від 0 до 21")]
        public int MinAge { get; set; }

        [Range(0, 10, ErrorMessage = "Рейтинг повинен бути від 0 до 10")]
        public double Rating { get; set; }

        [Required(ErrorMessage = "Статус обов'язковий")]
        public int StatusId { get; set; }

        public string PosterURL { get; set; }

        [Url(ErrorMessage = "Некоректне посилання на трейлер")]
        public string TrailerURL { get; set; }

        // data to map:
        public string? GenreName { get; set; }
        public string? StatusName { get; set; }
    }
}
