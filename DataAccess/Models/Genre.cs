namespace DataAccess.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Зв'язок one-to-many
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
