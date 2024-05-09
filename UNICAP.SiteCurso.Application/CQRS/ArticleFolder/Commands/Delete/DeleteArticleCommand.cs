using MediatR;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Delete
{
    public class DeleteArticleCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
