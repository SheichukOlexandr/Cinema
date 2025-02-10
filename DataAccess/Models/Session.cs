namespace DataAccess.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MoviePriceId { get; set; }

        public int RoomId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        // Зв'язки
        public virtual Room Room { get; set; }
        public virtual MoviePrice MoviePrice { get; set; }

        // Зв'язок one-to-many
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Movie Movie => MoviePrice?.Movie; // Навігаційна властивість через MoviePrice
    }
}