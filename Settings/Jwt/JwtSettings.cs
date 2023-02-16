namespace JwtDotNet7.Settings.Jwt
{
    public class JwtSettings : IJwtSettings
    {
        public const String SectionName = "JwtSettings";
        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
        public string ExpiryInMinutes { get; set; } = String.Empty;
        public string JwtSecret { get; set; } = String.Empty;
    }
}