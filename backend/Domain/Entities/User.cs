namespace Lab.Api.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? Role { get; set; }
    }
}
