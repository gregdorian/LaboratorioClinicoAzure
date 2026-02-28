using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;
using Lab.Api.Application.DTOs;
using Lab.Api.Application.Security;
using Lab.Api.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lab.Api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly LabDbContext _db;
        private readonly IJwtService _jwt;

        public UserService(LabDbContext db, IJwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<long> RegisterAsync(RegisterRequest request)
        {
            var exists = await _db.Users.AnyAsync(u => u.Username == request.Username);
            if (exists) throw new InvalidOperationException("Username already exists");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Role = request.Role,
                PasswordHash = PasswordHasher.Hash(request.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }

        public async Task<AuthResponse?> AuthenticateAsync(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) return null;
            if (!PasswordHasher.Verify(request.Password, user.PasswordHash)) return null;

            var token = _jwt.GenerateToken(user.Username, user.Role);
            return new AuthResponse { Token = token, Username = user.Username, Role = user.Role };
        }
    }
}
