using FitnessFlowBackend.Data;
using FitnessFlowBackend.DTOs;
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

        public async Task<IEnumerable<TrainingStatsDTO>> GetTrainingStatsByUserIdAsync(Guid userId, int? year, int? month, int? week)
        {
            var query = _context.Trainings.Where(t => t.UserId == userId);

            if (year.HasValue)
                query = query.Where(t => t.TrainingDateTime.Year == year.Value);

            if (month.HasValue)
                query = query.Where(t => t.TrainingDateTime.Month == month.Value);

            var trainings = await query.ToListAsync();

            if (week.HasValue)
            {
                trainings = trainings.Where(t => GetWeekOfMonth(t.TrainingDateTime) == week.Value).ToList();
            }

            var stats = trainings
                .GroupBy(t => GetWeekOfMonth(t.TrainingDateTime))
                .Select(g => new TrainingStatsDTO
                {
                    WeekNumber = g.Key,
                    TotalDurationInSeconds = g.Sum(t => t.DurationInSeconds),
                    NumberOfTrainings = g.Count(),
                    AverageIntensity = g.Average(t => t.Intensity),
                    AverageFatigue = g.Average(t => t.Fatigue)
                })
                .ToList();
        
            return stats;
        }

        private int GetWeekOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            return (int)Math.Ceiling((date.Day + (int)firstDayOfMonth.DayOfWeek) / 7.0);
        }


    }
}
