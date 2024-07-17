using FitnessFlowBackend.Data;
using FitnessFlowBackend.Models;
using FitnessFlowBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessFlowBackend.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FitnessFlowDbContext _context;

        public UserRepository(FitnessFlowDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
