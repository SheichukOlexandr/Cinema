using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class PasswordService
    {
        private readonly IPasswordHasher<object> _passwordHasher;
        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<object>();
        }
       
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
