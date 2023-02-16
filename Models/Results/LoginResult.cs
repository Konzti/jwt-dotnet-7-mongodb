namespace JwtDotNet7.Models.Results
{
    public record LoginResult
    {
        public bool Success { get; init; } = false;
        public string? Error { get; init; } = null;
        public string? Token { get; init; } = null;

    }
}
