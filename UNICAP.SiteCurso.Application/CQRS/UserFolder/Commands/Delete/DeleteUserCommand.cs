using MediatR;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Delete
{
    public class DeleteUserCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
