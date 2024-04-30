using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Entities;
using UNICAP.SiteCurso.Domain.Exceptions;

namespace UNICAP.SiteCurso.Application.CQRS.AuthFolder.Commands.SignIn
{
    public class SignInHandler : IRequestHandler<SignInCommand, Response>
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IEFContext eFContext;
        private Response response;
        
        public SignInHandler(ITokenGenerator tokenGenerator, IEFContext eFContext, Response response)
        {
            this.tokenGenerator = tokenGenerator;
            this.eFContext = eFContext;
            this.response = response;
        }

        public async Task<Response> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await GetUser(request.Login, request.Password);

            ValidateUser(user);

            var jwt = await GetJsonWebToken(user);


            return await response.GenerateResponse(statusCode: HttpStatusCode.OK, message: "Usuário autenticado com sucesso", collection: jwt, count: 1);
        }

        private async Task<JsonWebToken> GetJsonWebToken(User user)
        {
            var jwt = await tokenGenerator.GenerateToken(user);

            await eFContext.SaveChangesAsync(); 

            tokenGenerator.SetSessionUser(jwt);

            return jwt;
        }

        private void ValidateUser(User user)
        {
            if (user is null)
                throw new NotFoundException("Usuário não encontrado.");

            if (!user.IsActive)
                throw new Exception("Usuário não está ativo no Sistema!");
        }

        private async Task<User> GetUser(string login, string password)
        {
            var user = await eFContext.Users.FirstOrDefaultAsync(x => x.IsActive && x.Credentials.Login == login);

            bool isAuth = user != null && user.Credentials.isAuthentic(password);

            if (!isAuth)
                throw new UnauthorizedException("Usuário ou senha incorretos.");

            return user;

        }
    }
}
