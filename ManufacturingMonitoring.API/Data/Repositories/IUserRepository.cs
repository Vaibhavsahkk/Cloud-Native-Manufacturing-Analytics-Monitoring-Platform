using ManufacturingMonitoring.API.Models;

namespace ManufacturingMonitoring.API.Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
