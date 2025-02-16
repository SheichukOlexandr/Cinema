namespace DataAccess.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        //Зв'язок one-to-many
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    }
}
