using BCrypt.Net;
using FitnessFlowBackend.Models;
using FitnessFlowBackend.Repositories.Interfaces;
using FitnessFlowBackend.Services.Interfaces;

namespace FitnessFlowBackend.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)) return null;

            return user;
        }

        public async Task<User?> Register(string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);

            if (existingUser != null) return null;

            var user = new User
            {
                Email = email,
                Password = HashPassword(password)
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
