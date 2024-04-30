using MediatR;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetById
{
    public class GetUserByIdQuery : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
