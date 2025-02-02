using DataAccess.Models;

namespace BusinessLogic.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public int SeatId { get; set; }
        public int StatusId { get; set; }

        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
        public virtual Seat Seat { get; set; }

        // data to map:
        public string? UserFullName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? MovieName { get; set; }
        public int SeatNumber { get; set; }
        public string? RoomName { get; set; }
        public string? StatusName { get; set; }
    }
}
