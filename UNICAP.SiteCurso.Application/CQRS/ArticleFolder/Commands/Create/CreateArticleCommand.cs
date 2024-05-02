using MediatR;
using UNICAP.SiteCurso.Application.CQRS.BaseFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Create
{
    public class CreateArticleCommand : CreateBaseCommand, IRequest<Response>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int UserId { get; set; }
    }
}
