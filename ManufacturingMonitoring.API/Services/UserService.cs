using ManufacturingMonitoring.API.Data;
using ManufacturingMonitoring.API.Data.Repositories;
using ManufacturingMonitoring.API.DTOs;
using ManufacturingMonitoring.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingMonitoring.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserService(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto?> CreateUser(CreateUserRequestDto request)
        {
            // Check if role exists (we'll seed roles first, or create inline)
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name.ToLower() == request.Role.ToLower());

            if (role == null)
            {
                return null;
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                RoleId = role.Id,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = role.Name,
                IsActive = user.IsActive
            };

            return response;
        }

        public async Task<List<UserResponseDto>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();

            var userDtos = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role?.Name ?? "Unknown",
                IsActive = u.IsActive
            }).ToList();

            return userDtos;
        }

        public async Task<LoginResponseDto?> ValidateLogin(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email.ToLower());

            if (user == null)
            {
                return null;
            }

            // Dummy password check - accepts any password for now
            var response = new LoginResponseDto
            {
                UserId = user.Id,
                Name = user.Name,
                Role = user.Role?.Name ?? "Unknown",
                Token = $"dummy-token-{Guid.NewGuid()}"
            };

            return response;
        }
    }
}
