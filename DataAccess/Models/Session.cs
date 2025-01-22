namespace DataAccess.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int MoviePriceId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        // Зв'язки
        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
        public virtual MoviePrice MoviePrice { get; set; }

        // Зв'язок one-to-many
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}