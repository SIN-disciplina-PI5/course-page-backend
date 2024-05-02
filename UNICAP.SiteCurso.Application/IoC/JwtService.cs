using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using System.Text;
using UNICAP.SiteCurso.Application.Interfaces;

namespace UNICAP.SiteCurso.Application.IoC
{
    public class JwtService : IJwtService
    {
        private readonly IHttpContextAccessor httpAccessor;

        public JwtService(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor;
        }

        public object GetUserLoggedAtSession()
        {
            object Jwt = null;
            var jsonStr = httpAccessor.HttpContext.Session.GetString("UserLogged");
            if (!string.IsNullOrEmpty(jsonStr))
            {
                Jwt = JsonConvert.DeserializeObject(jsonStr);
                if (Jwt != null)
                {
                    return (JsonWebToken)Jwt;
                }
            }

            return Jwt;

        }
    }
}
