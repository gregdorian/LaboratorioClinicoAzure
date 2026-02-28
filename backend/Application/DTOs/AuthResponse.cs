namespace Lab.Api.Application.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Role { get; set; }
    }
}
