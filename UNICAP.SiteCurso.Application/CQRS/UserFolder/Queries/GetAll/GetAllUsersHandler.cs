using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.DTOs.UserFolder;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

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
            var list = eFContext.Users.Where(x => request.WithDisabled ? true : x.IsActive).OrderBy(x => x.UpdatedAt).ToList();

            if (list is null)
            {
                response.AddErrorMessages(message: Messages.RegisterNotFoundMessage);
                return await response.GenerateResponse(statusCode: HttpStatusCode.BadRequest, hasError: true, message: Messages.RegisterNotFoundMessage);

            }

            if (request.WithPagination)
            {
                int countTotal = list.Count;
                int totalPages = (int)countTotal / request.ItensPerPage;
                int resto = (int)countTotal % request.ItensPerPage;
                if (resto != 0) totalPages++;

                var listPaginate = list.Skip(request.ItensPerPage * (request.CurrentPage - 1)).Take(request.ItensPerPage);
                List<UserDTO> listPaginateDto = mapper.Map<List<UserDTO>>(listPaginate.ToList());
                Paginator<UserDTO> paginator = new Paginator<UserDTO> { ItensPerPage = request.ItensPerPage, CurrentPage = request.CurrentPage, Itens = listPaginateDto, TotalItens = countTotal, TotalPages = totalPages };

                return await response.GenerateResponse(statusCode: HttpStatusCode.OK, collection: paginator, message: "Resultado obtido com sucesso.", count: paginator.Itens.Count);

            }
            else
            {
                var itensDto = mapper.Map<List<UserDTO>>(list);
                var paginator = new Paginator<UserDTO> { CurrentPage = 1, Itens = itensDto, ItensPerPage = itensDto.Count, TotalPages = 1, TotalItens = itensDto.Count };

                return await response.GenerateResponse(statusCode: HttpStatusCode.OK, collection: paginator, message: "Resultado obtido com sucesso.", count: paginator.Itens.Count);
            }

        }
    }
}
