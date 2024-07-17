﻿namespace FitnessFlowBackend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Training> Trainings { get; set; }
    }
}
