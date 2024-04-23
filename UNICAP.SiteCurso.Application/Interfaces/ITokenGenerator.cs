using Microsoft.IdentityModel.JsonWebTokens;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Domain.Entities;

namespace UNICAP.SiteCurso.Application.Interfaces
{
    public interface ITokenGenerator
    {
        Task<JsonWebToken> GenerateToken(User user);
        void SetSessionUser(JsonWebToken jwt);

    }
}
