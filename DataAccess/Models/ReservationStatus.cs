namespace DataAccess.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // One-to-Many: ReservationStatus -> Reservations
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Constants for predefined reservation statuses
        public const string Created = "Створено";
        public const string Confirmed = "Підтверджено";
        public const string Completed = "Завершено";
        public const string Cancelled = "Скасовано";
    }
}