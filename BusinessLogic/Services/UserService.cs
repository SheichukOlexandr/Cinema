using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly PasswordService _passwordService;

        public UserService(IGenericRepository<User> userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            user.Password = _passwordService.HashPassword(password);
            await _userRepository.AddAsync(user);
        }

        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return false;
            }
            return _passwordService.VerifyPassword(user.Password, password);
        }

        // Тимчасовий метод для тестування хешування паролів.
        public async Task<string> RegisterUserAndCheckPasswordAsync(User user, string password)
        {
            user.Password = _passwordService.HashPassword(password);
            await _userRepository.AddAsync(user);
            return user.Password;
        }
    }
}
