namespace FitnessFlowBackend.DTOs
{
    public class TrainingStatsDTO
    {
        public int WeekNumber { get; set; }
        public int TotalDurationInSeconds { get; set; }
        public int NumberOfTrainings { get; set; }
        public double AverageIntensity { get; set; }
        public double AverageFatigue { get; set; }
    }
}
