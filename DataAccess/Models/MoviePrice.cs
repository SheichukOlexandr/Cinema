namespace DataAccess.Models
{
    public class MoviePrice
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }

        public virtual required Movie Movie { get; set; }

        // One-to-Many: MoviePrice -> Sessions
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}

