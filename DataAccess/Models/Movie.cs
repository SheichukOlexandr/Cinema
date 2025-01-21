namespace DataAccess.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public TimeSpan Duration { get; set; }
        public string Cast { get; set; }
        public int GenreId { get; set; }   
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public int MinAge { get; set; }
        public double Rating { get; set; }
        public int StatusId { get; set; }


        public Genre Genre { get; set; }
        public MovieStatus Status { get; set; }

    }
}
