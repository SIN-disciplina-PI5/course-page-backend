using MediatR;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetAll
{
    public class GetAllUserQuery : IRequest<Response>
    {
        public GetAllUserQuery(bool withDisabled)
        {
            WithDisabled = withDisabled;
        }

        public bool WithDisabled { get; set; }
    }
}
