namespace Lab.Api.Application.DTOs
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
