using RoadReady.API.DTO;
namespace RoadReady.API.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<bool> UpdateUserAsync(int userId, UpdateUserDto dto);

    }
}
