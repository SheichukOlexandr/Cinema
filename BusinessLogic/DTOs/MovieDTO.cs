namespace BusinessLogic.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public string Cast { get; set; }
        public int GenreId { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Description { get; set; }
        public int MinAge { get; set; }
        public double Rating { get; set; }
        public int StatusId { get; set; }
        public string PosterURL { get; set; }
        public string TrailerURL { get; set; }

        // data to map:
        public string? GenreName { get; set; }
        public string? StatusName { get; set; }
    }
}
