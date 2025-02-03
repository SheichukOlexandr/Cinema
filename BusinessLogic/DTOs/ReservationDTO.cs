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

        // data to map:
        public string? UserFullName { get; set; }
        public SessionDTO? Session { get; set; }
        public int SeatNumber { get; set; }
        public decimal SeatExtraPrice { get; set; }
        public string? StatusName { get; set; }
    }
}
