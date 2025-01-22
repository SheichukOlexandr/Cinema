namespace DataAccess.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public int SeatId { get; set; }
        public int StatusId { get; set; }

        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual ReservationStatus Status { get; set; }

        public Movie Movie => Session?.Movie; // Навігаційна властивість через Session

    }
}
