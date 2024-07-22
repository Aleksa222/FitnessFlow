using FitnessFlowBackend.DTOs;
using FitnessFlowBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FitnessFlowBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TrainingDTO trainingDTO)
        {
            var training = await _trainingService.Add(trainingDTO.UserId, trainingDTO.TrainingType,
                trainingDTO.DurationInSeconds, trainingDTO.CaloriesBurned, trainingDTO.Intensity,
                trainingDTO.Fatigue, trainingDTO.Notes, trainingDTO.TrainingDateTime);

            if (training == null) return BadRequest("There was an error in creating the training");

            return Ok(training);
        }

        [HttpGet("/get/{userId}")]
        public async Task<IActionResult> GetTrainingsByUserId(Guid userId)
        {
            var trainings = await _trainingService.GetByUserIdAsync(userId);
            return Ok(trainings);
        }

        [HttpGet("get/{userId}/stats")]
        public async Task<IActionResult> GetTrainingStatsByUser(Guid userId, int? year, int? month, int? week)
        {
            var statistics = await _trainingService.GetTrainingStatsByUserIdAsync(userId, year, month, week);
            return Ok(statistics);
        }
    }
}
