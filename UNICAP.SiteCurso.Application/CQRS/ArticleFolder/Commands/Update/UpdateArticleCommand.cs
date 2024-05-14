using MediatR;
using UNICAP.SiteCurso.Application.CQRS.BaseFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Update
{
    public class UpdateArticleCommand : UpdateBaseCommand, IRequest<Response>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
