using FitnessFlowBackend.DTOs;
using FitnessFlowBackend.Enums;
using FitnessFlowBackend.Models;
using FitnessFlowBackend.Repositories.Interfaces;
using FitnessFlowBackend.Services.Interfaces;

namespace FitnessFlowBackend.Services.Implementations
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;

        public TrainingService(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Training?> Add(Guid userId, TrainingType type, int durationInSeconds,
            int caloriesBurned, int intensity, int fatigue, string notes, DateTime trainingDateTime)
        {
            var training = new Training
            {
                UserId = userId,
                TrainingType = type,
                DurationInSeconds = durationInSeconds,
                CaloriesBurned = caloriesBurned,
                Intensity = intensity,
                Fatigue = fatigue,
                Notes = notes,
                TrainingDateTime = trainingDateTime
            };

            await _trainingRepository.AddAsync(training);
            await _trainingRepository.SaveChangesAsync();

            return training;
        }

        public async Task<IEnumerable<Training>> GetByUserIdAsync(Guid userId)
        {
            return await _trainingRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<TrainingStatsDTO>> GetTrainingStatsByUserIdAsync(Guid userId, int? year, int? month, int? week)
        {
            return await _trainingRepository.GetTrainingStatsByUserIdAsync(userId, year, month, week);
        }

    }
}
