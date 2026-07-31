using ManufacturingMonitoring.API.DTOs;

namespace ManufacturingMonitoring.API.Services
{
    public interface IUserService
    {
        Task<UserResponseDto?> CreateUser(CreateUserRequestDto request);
        Task<List<UserResponseDto>> GetUsers();
        Task<LoginResponseDto?> ValidateLogin(LoginRequestDto request);
    }
}
