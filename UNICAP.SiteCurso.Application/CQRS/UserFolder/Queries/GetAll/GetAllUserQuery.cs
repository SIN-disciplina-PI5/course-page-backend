using MediatR;
using UNICAP.SiteCurso.Application.CQRS.BaseFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetAll
{
    public class GetAllUserQuery : PaginatorQueryBase, IRequest<Response>
    {
        public GetAllUserQuery(bool withDisabled, bool withPagination, int currentPage, int itensPerPage)
        {
            WithDisabled = withDisabled;
            WithPagination = withPagination;
            CurrentPage = currentPage;
            ItensPerPage = itensPerPage;
        }
    }
}
