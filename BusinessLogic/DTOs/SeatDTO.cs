namespace BusinessLogic.DTOs
{
    public class SeatDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int Number { get; set; }
        public decimal ExtraPrice { get; set; }

        // data to map:
        public string? RoomName { get; set; }
        public string? SeatName { get; set; }
    }
}
