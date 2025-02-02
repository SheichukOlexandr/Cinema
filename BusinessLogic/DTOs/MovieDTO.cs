using System.ComponentModel.DataAnnotations;

public class MovieDTO
{
    [Required(ErrorMessage = "Назва фільму є обов'язковою.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Режисер є обов'язковим.")]
    public string Director { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Тривалість фільму повинна бути більшою за 0.")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "Акторський склад є обов'язковим.")]
    public string Cast { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Жанр є обов'язковим.")]
    public int GenreId { get; set; }

    [Required(ErrorMessage = "Дата релізу є обов'язковою.")]
    public DateOnly ReleaseDate { get; set; }

    [Required(ErrorMessage = "Опис є обов'язковим.")]
    public string Description { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Мінімальний вік не може бути від'ємним.")]
    public int MinAge { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Статус фільму є обов'язковим.")]
    public int StatusId { get; set; }

    [Url(ErrorMessage = "Посилання на постер має бути дійсним URL.")]
    public string PosterURL { get; set; }

    [Url(ErrorMessage = "Посилання на трейлер має бути дійсним URL.")]
    public string TrailerURL { get; set; }
}