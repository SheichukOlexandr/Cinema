namespace DataAccess.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int MoviePriceId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public Movie Movie { get; set; }
        public Room Room { get; set; }
        public MoviePrice MoviePrice { get; set; }
    }
}