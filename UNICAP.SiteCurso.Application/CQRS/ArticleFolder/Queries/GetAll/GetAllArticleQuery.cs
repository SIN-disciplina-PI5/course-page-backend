using MediatR;
using UNICAP.SiteCurso.Application.CQRS.BaseFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Queries.GetAll
{
    public class GetAllArticleQuery : PaginatorQueryBase, IRequest<Response>
    {
        public GetAllArticleQuery(bool withDisabled, bool withPagination, int currentPage, int itensPerPage)
        {
            WithDisabled = withDisabled;
            WithPagination = withPagination;
            CurrentPage = currentPage;
            ItensPerPage = itensPerPage;
        }
    }
}
