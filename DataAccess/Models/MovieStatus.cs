namespace DataAccess.Models
{
    public class MovieStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // One-to-many: MovieStatus -> Movies
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>(); 
    }
}
