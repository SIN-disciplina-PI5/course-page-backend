using Microsoft.IdentityModel.JsonWebTokens;

namespace UNICAP.SiteCurso.Application.Interfaces
{
    public interface ITokenGenerator
    {
        //Task<JsonWebToken> GenerateToken(User user);
        void SetSessionUser(JsonWebToken jwt);

    }
}
