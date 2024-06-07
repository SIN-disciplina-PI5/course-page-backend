using MediatR;
using UNICAP.SiteCurso.Application.CQRS.BaseFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetAll
{
    public class GetAllUserQuery :  IRequest<Response>
    {
        public GetAllUserQuery(bool withDisabled)
        {
            this.withDisabled = withDisabled;
        }

        public bool withDisabled { get; set; }
    }
}
