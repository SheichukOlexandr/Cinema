namespace DataAccess.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int Number { get; set; }
        public Decimal ExtraPrice { get; set; }

        public Room Room { get; set; }

    }
}
