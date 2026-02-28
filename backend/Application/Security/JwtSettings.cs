namespace Lab.Api.Application.Security
{
    public class JwtSettings
    {
        public string Key { get; set; } = null!;
        public int ExpiresMinutes { get; set; } = 60;
    }
}
