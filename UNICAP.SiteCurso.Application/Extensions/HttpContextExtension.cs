using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetToken(this HttpContext http)
        {
            return http.GetTokenAsync("access_token").Result;
        }

        public static string GetPerfil(this HttpContext http)
        {
            return http.User != null && http.User.Claims != null && http.User.Claims.Where(q => q.Type.Contains("claims/role")).Count() > 0 ? http.User.Claims.Where(q => q.Type.Contains("claims/role")).FirstOrDefault().Value : Messages.SessionNotFound;
        }

        public static string GetLogin(this HttpContext http)
        {
            return http != null ? http.User.Identity.Name : Messages.SessionNotFound;
        }

        public static string GetUserData(this HttpContext http)
        {
            return http.User.Claims.Where(q => q.Type.Contains("claims/userdata")).FirstOrDefault()?.Value ?? null;
        }
    }
}
