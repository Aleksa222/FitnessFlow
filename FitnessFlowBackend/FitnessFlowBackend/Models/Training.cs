using FitnessFlowBackend.Enums;

namespace FitnessFlowBackend.Models
{
    public class Training
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public TrainingType TrainingType { get; set; }
        public int DurationInSeconds { get; set; }
        public int CaloriesBurned { get; set; }
        public int Intensity { get; set; }
        public int Fatigue { get; set; }
        public string Notes { get; set; }
        public DateOnly TrainingDate { get; set; }
        public TimeOnly TrainingTime { get; set; }


    }
}
