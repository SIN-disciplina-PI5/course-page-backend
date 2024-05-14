using MediatR;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Queries.GetById
{
    public class GetArticleByIdQuery : IRequest<Response>
    {
        public GetArticleByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
