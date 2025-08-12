using RoadReady.API.Models;
namespace RoadReady.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> SaveChangesAsync();
        Task<User> GetByIdAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();
        
    }
}