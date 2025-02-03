namespace BusinessLogic.DTOs
{
    public class SeatDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int Number { get; set; }
        public Decimal ExtraPrice { get; set; }

        // data to map:
        public string? RoomName { get; set; }

    }
}
