namespace DataAccess.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int StatusId { get; set; }

        // data to map:
        public string? StatusName { get; set; }
    }
}
