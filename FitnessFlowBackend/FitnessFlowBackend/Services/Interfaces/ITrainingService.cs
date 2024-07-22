using FitnessFlowBackend.DTOs;
using FitnessFlowBackend.Enums;
using FitnessFlowBackend.Models;

namespace FitnessFlowBackend.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<Training?> Add(Guid userId, TrainingType type, int durationInSeconds, int caloriesBurned,
            int intensity, int fatigue, string notes, DateTime trainingDateTime);

        Task<IEnumerable<Training>> GetByUserIdAsync(Guid userId);

        Task<IEnumerable<TrainingStatsDTO>> GetTrainingStatsByUserIdAsync(Guid userId, int? year, int? month, int? week);

    }
}
