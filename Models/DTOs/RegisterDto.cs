namespace JwtDotNet7.Models.DTOs
{
    public record RegisterDto
    {
        public string FirstName { get; init; } = String.Empty;
        public string LastName { get; init; } = String.Empty;
        public string Email { get; init; } = String.Empty;
        public string Password { get; init; } = String.Empty;

    }
}