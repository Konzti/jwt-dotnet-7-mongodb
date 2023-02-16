namespace JwtDotNet7.Models.Results
{
    public record RegisterResult
    {
        public bool Success { get; init; } = false;
        public string? Error { get; init; } = null;
        public User? User { get; init; } = null;

    }
}