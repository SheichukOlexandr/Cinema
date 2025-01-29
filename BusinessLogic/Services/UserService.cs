using DataAccess.Repositories.UnitOfWork;
using DataAccess.Models;
using System.Security.Cryptography;
using System.Text;
namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task RegisterUserAsync(User user)
        {
            user.Password = HashPassword(user.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = await _unitOfWork.Users.GetAllAsync(u => u.Email == email && u.Password == hashedPassword);
            return user.FirstOrDefault();
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}