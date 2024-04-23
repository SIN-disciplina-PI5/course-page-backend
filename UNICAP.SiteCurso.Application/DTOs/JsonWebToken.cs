namespace UNICAP.SiteCurso.Application.DTOs
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
