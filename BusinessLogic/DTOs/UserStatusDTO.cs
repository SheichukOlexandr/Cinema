using DataAccess.Models;

namespace BusinessLogic.DTOs
{
    public class UserStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constants for predefined users statuses
        public const string Active = UserStatus.Active;
        public const string Admin = UserStatus.Admin;
        public const string Blocked = UserStatus.Blocked;
    }
}
