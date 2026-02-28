using Lab.Api.Application.DTOs;

namespace Lab.Api.Application.Services
{
    public interface IUserService
    {
        Task<AuthResponse?> AuthenticateAsync(LoginRequest request);
        Task<long> RegisterAsync(RegisterRequest request);
    }
}
