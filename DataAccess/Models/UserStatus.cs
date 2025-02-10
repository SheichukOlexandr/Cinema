namespace DataAccess.Models
{
    public class UserStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        // Constants for predefined users statuses
        public const string Active = "Активний";
        public const string Admin = "Адміністратор";
        public const string Blocked = "Заблокований";
    }
}
