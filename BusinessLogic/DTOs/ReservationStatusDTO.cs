using DataAccess.Models;

namespace BusinessLogic.DTOs
{
    public class ReservationStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constants for predefined reservation statuses
        public const string Created = ReservationStatus.Created;
        public const string Confirmed = ReservationStatus.Confirmed;
        public const string Completed = ReservationStatus.Completed;
        public const string Cancelled = ReservationStatus.Cancelled;
    }
}