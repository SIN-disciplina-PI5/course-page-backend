using MediatR;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.AuthFolder.Commands.SignIn
{
    public class SignInCommand : IRequest<Response>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
