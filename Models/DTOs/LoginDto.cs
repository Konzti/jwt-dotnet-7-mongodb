namespace JwtDotNet7.Models.DTOs
{
    public record LoginDto
    {
        public string Email { get; init; } = String.Empty;
        public string Password { get; init; } = String.Empty;
    }
}