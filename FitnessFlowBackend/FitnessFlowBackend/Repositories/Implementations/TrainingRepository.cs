using FitnessFlowBackend.Data;
using FitnessFlowBackend.Models;
using FitnessFlowBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessFlowBackend.Repositories.Implementations
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly FitnessFlowDbContext _context;

        public TrainingRepository(FitnessFlowDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Training>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Trainings.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<Training> GetByIdAsync(Guid id)
        {
            return await _context.Trainings.FindAsync(id);
        }
        public async Task AddAsync(Training training)
        {
            await _context.Trainings.AddAsync(training);
        }
        public async Task UpdateAsync(Training training)
        {
            _context.Trainings.Update(training);
        }
        public async Task DeleteAsync(Training training)
        {
            _context.Trainings.Remove(training);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
