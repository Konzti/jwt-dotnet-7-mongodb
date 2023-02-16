namespace JwtDotNet7.Settings.Jwt
{
    public interface IJwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiryInMinutes { get; set; }
        public string JwtSecret { get; set; }
    }
}