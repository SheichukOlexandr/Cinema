namespace DataAccess.Models
{
    public class SessionDTO
    {
        public int Id { get; set; }
        public int MoviePriceId { get; set; }
        public int RoomId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        // data to map:
        public string? RoomName { get; set; }
        public decimal Price { get; set; }
        public string? MovieName { get; set; }
    }
}