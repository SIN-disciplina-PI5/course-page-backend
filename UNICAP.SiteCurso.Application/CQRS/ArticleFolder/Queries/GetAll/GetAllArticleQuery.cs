using MediatR;
using UNICAP.SiteCurso.Application.CQRS.BaseFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Queries.GetAll
{
    public class GetAllArticleQuery : IRequest<Response>
    {
        public GetAllArticleQuery(bool withDisabled)
        {
            this.withDisabled = withDisabled;
        }

        public bool withDisabled { get; set; }
    }
}
