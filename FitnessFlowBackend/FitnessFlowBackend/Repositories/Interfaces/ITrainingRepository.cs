using FitnessFlowBackend.Models;

namespace FitnessFlowBackend.Repositories.Interfaces
{
    public interface ITrainingRepository
    {
        Task<IEnumerable<Training>> GetByUserIdAsync(int userId);
        Task<Training> GetByIdAsync(int id);
        Task AddAsync(Training training);
        Task UpdateAsync(Training training);
        Task DeleteAsync(Training training);
        Task SaveChangesAsync();
    }
}
