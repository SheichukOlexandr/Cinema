namespace DataAccess.Models
{
    public class MoviePrice
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }

        public Movie Movie { get; set; }

    }
}

