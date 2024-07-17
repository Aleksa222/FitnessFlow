using FitnessFlowBackend.Models;

namespace FitnessFlowBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> Login(string email, string password);
        Task<User?> Register(string email, string password);
    }
}
