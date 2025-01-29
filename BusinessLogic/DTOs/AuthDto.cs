namespace BusinessLogic.DTOs
{
    public class AuthDto
    {
        // Login properties
        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }
        // Register properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegisterEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string RegisterPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}