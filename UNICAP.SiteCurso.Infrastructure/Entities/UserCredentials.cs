using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UNICAP.SiteCurso.Infrastructure.Enum;

namespace UNICAP.SiteCurso.Infrastructure.Entities
{
    [Table("TB_USER_CREDENTIALS")]
    public class UserCredentials : BaseEntity<int>
    {
        [Column("LOGIN")]
        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string Login { get; set; }

        private string _password;

        [Column("SENHA")]
        [Required]
        public string Password
        {
            get => _password;
            set
            {
                Salt = GenerateRandomSalt();
                _password = Hash(value, Salt);
            }
        }

        [Column("SALT")]
        [Required]
        public string Salt { get; private set; }

        [Column("CARGO")]
        [Required]
        public Role Role { get; set; }

        [Column("REFRESH_TOKEN")]
        public string RefreshToken { get; set; }

        [Column("REFRESH_TOKEN_EXPIRATION")]
        public DateTime RefreshTokenExpiration { get; set; }

        [Column("FORGOT_PASSWORD_TOKEN")]
        public string ForgotPasswordToken { get; private set; }

        [Column("FORGOT_PASSWORD_URL")]
        public string ForgotPassworlUrl { get; set; }

        [Column("FORGOT_PASSWORD_EXPIRATION")]
        public DateTime ForgotTokenExpiration { get; set; }

        [Column("ULTIMO_ACESSO")]
        public DateTime? LastAcess { get; set; }

        public string GenerateForgotPasswordToken()
        {
            using var Algorithm = SHA256.Create();
            var bytes = Algorithm.ComputeHash(Encoding.UTF8.GetBytes(GenerateRandomSalt().Substring(0, 6)));
            var builder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            ForgotPasswordToken = builder.ToString();
            return ForgotPasswordToken;
        }

        public bool isAuthentic(string rawPassword)
        {
            var hashPassword = Hash(rawPassword, Salt);
            return Password.Equals(hashPassword);
        }

        private string Hash(string rawPassword, string salt)
        {
            using var Algorithm = SHA256.Create();
            var bytes = Algorithm.ComputeHash(Encoding.UTF8.GetBytes(rawPassword + salt));
            var builder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        private string GenerateRandomSalt()
        {
            Regex rgx = new Regex("[a-zA-Z0-9]");
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return String.Join("", rgx.Matches(Convert.ToBase64String(salt)).Cast<Match>().Select(m => m.Value).ToArray());
        }

    }
}
