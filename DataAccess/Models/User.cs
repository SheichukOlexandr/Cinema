namespace DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public int StatusId { get; set; }

        // Зв'язки
        public virtual UserStatus Status { get; set; }

        // one-to-many: User -> Reservation
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();


    }
}
