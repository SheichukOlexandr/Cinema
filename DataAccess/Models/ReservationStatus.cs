namespace DataAccess.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // One-to-Many: ReservationStatus -> Reservations
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}