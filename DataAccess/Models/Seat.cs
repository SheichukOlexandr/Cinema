namespace DataAccess.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int Number { get; set; }
        public Decimal ExtraPrice { get; set; }

        // Зв'язок many-to-one Seat -> Room
        public virtual Room Room { get; set; }

        // One-to-many: Seat -> Reservations
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
