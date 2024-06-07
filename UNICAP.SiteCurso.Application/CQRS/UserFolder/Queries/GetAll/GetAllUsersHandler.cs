using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.DTOs.UserFolder;
using UNICAP.SiteCurso.Application.Interfaces;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetAll
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUserQuery, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IMapper mapper;
        private readonly Response response;

        public GetAllUsersHandler(IEFContext eFContext, IMapper mapper, Response response)
        {
            this.eFContext = eFContext;
            this.mapper = mapper;
            this.response = response;
        }

        public async Task<Response> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var list = eFContext.Users.Where(x => request.withDisabled ? true : x.IsActive).OrderBy(x => x.UpdatedAt).ToList();
            var userDTO = mapper.Map<List<UserDTO>>(list);
            return await response.GenerateResponse(statusCode: HttpStatusCode.OK, collection: userDTO, message: "Resultado obtido com sucesso.", count: userDTO.Count);

        }
    }
}
