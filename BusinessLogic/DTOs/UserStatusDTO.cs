namespace BusinessLogic.DTOs
{
    public class UserStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constants for predefined users statuses
        public const string Active = "Активний";
        public const string Admin = "Адміністратор";
        public const string Blocked = "Заблокований";
    }
}
