public class MovieDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Director { get; set; }
    public int Duration { get; set; }
    public required string Cast { get; set; }
    public int GenreId { get; set; }
    public string GenreName { get; set; } // Додаємо назву жанру
    public DateOnly ReleaseDate { get; set; }
    public required string Description { get; set; }
    public int MinAge { get; set; }
    public double Rating { get; set; }
    public int StatusId { get; set; }
    public required string PosterURL { get; set; }
    public required string TrailerURL { get; set; }
}
