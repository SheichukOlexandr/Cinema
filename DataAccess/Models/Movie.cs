namespace DataAccess.Models
{
    public class Movie
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
        public string BannerURL { get; set; }
        public string TrailerURL { get; set; }

        // зв'язки
        public virtual Genre Genre { get; set; }
        public virtual MovieStatus Status { get; set; }


        // One-to-Many: Movie -> MoviePrice
        public virtual ICollection<MoviePrice> MoviePrices { get; set; } = new List<MoviePrice>();
    }
}
