using MediatR;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Queries.GetAll
{
    public class GetAllArticleQuery : IRequest<Response>
    {
        public GetAllArticleQuery(bool withDisabled)
        {
            WithDisabled = withDisabled;
        }

        public bool WithDisabled { get; set; }
    }
}
