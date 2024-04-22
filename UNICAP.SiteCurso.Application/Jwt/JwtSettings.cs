namespace UNICAP.SiteCurso.Application.Jwt
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string[] Audience { get; set; }
        public int RefreshTokenExpirationInMinutes { get; set; }
    }
}
