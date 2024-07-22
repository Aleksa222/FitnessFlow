using FitnessFlowBackend.Enums;

namespace FitnessFlowBackend.DTOs
{
    public class TrainingDTO
    {
        public Guid UserId { get; set; }
        public TrainingType TrainingType { get; set; }
        public int DurationInSeconds { get; set; }
        public int CaloriesBurned { get; set; }
        public int Intensity {  get; set; }
        public int Fatigue {  get; set; }
        public string Notes { get; set; }
        public DateTime TrainingDateTime { get; set; }

    }
}
