using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Entities;
using UNICAP.SiteCurso.Domain.Exceptions;

namespace UNICAP.SiteCurso.Application.Jwt
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly JwtSettings jwtSettings;
        private readonly IHttpContextAccessor httpContextAccessor;


        public JwtGenerator(IOptionsSnapshot<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor)
        {
            this.jwtSettings = jwtSettings.Value;
            this.httpContextAccessor = httpContextAccessor;

        }

        public Task<JsonWebToken> GenerateToken(User user)
        {
            var claimsIn = GetUserClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.ExpirationInMinutes));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                claims: claimsIn,
                expires: expires,
                signingCredentials: creds
            );

            return Task.FromResult(new JsonWebToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = CreateRefreshToken(),
                ExpiresIn = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.ExpirationInMinutes))
            });
        }

        private List<Claim> GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Credentials.Login),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("nome", user.Nome),
                new Claim("cargo", ((int)user.Credentials.Role).ToString()), // Convertendo o enum para int
                new Claim(JwtRegisteredClaimNames.Aud, jwtSettings.Audience[0]),
                new Claim(JwtRegisteredClaimNames.Aud, jwtSettings.Audience[1])
            };

            return claims;
        }

        private RefreshToken CreateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                ExpiresIn = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.RefreshTokenExpirationInMinutes))
            };
            string token;
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.Token = token.Replace("+", string.Empty)
                            .Replace("=", string.Empty)
                            .Replace("/", string.Empty);
            return refreshToken;

        }

        public void SetSessionUser(JsonWebToken jwt)
        {
            if (jwt is null)
                throw new ForbiddenException("Ocorreu um erro ao gerar token de autenticação.");

            var jwtSerealized = JsonConvert.SerializeObject(jwt);
            httpContextAccessor.HttpContext.Session.SetString("UserLogged", jwtSerealized);
        }
    }
}
