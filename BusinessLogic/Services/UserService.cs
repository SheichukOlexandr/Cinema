using DataAccess.Repositories.UnitOfWork;
using System.Security.Cryptography;
using System.Text;
using BusinessLogic.DTOs;
using DataAccess.Models;
using AutoMapper;
using System.Security.Claims;

namespace BusinessLogic.Services
{
    public class UserService : BaseService<UserDTO, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork.Users, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterUserAsync(User user)
        {
            user.Password = HashPassword(user.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public override Task UpdateAsync(UserDTO user)
        {
            user.NewPassword = HashPassword(user.NewPassword);
            return base.UpdateAsync(user);
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !VerifyPassword(user.Password, password))
            {
                return null;
            }
            return user;
        }

        public async Task<UserStatus> GetOrCreateUserStatusAsync(string statusName)
        {
            var status = await _unitOfWork.UserStatuses.FirstOrDefaultAsync(s => s.Name == statusName);
            if (status == null)
            {
                status = new UserStatus { Name = statusName };
                await _unitOfWork.UserStatuses.AddAsync(status);
                await _unitOfWork.SaveChangesAsync();
            }
            return status;
        }

        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.Users.FirstOrDefaultAsync(s => s.Email == email));
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

        private bool VerifyPassword(string hashedPassword, string password)
        {
            var hashedInputPassword = HashPassword(password);
            return hashedPassword == hashedInputPassword;
        }

        public async Task<UserDTO?> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var emailClaim = claimsPrincipal.Claims.FirstOrDefault(it => it.Type == ClaimTypes.Email);
            if (emailClaim == null)
            {
                return null;
            }

            var email = emailClaim.Value;
            var user = await GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}