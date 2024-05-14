using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs;
using UNICAP.SiteCurso.Application.DTOs.ArticleFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Queries.GetAll
{
    public class GetAllArticleHandler : IRequestHandler<GetAllArticleQuery, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IMapper mapper;
        private readonly Response response;

        public GetAllArticleHandler(IEFContext eFContext, IMapper mapper, Response response)
        {
            this.eFContext = eFContext;
            this.mapper = mapper;
            this.response = response;
        }

        public async Task<Response> Handle(GetAllArticleQuery request, CancellationToken cancellationToken)
        {

            var list = eFContext.Articles.Where(x => request.WithDisabled ? true : x.IsActive).OrderBy(x => x.UpdatedAt).ToList();

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
                List<ArticleDTO> listPaginateDto = mapper.Map<List<ArticleDTO>>(listPaginate.ToList());
                Paginator<ArticleDTO> paginator = new Paginator<ArticleDTO> { ItensPerPage = request.ItensPerPage, CurrentPage = request.CurrentPage, Itens = listPaginateDto, TotalItens = countTotal, TotalPages = totalPages };

                return await response.GenerateResponse(statusCode: HttpStatusCode.OK, collection: paginator, message: "Resultado obtido com sucesso.", count: paginator.Itens.Count);

            }
            else
            {
                var itensDto = mapper.Map<List<ArticleDTO>>(list);
                var paginator = new Paginator<ArticleDTO> { CurrentPage = 1, Itens = itensDto, ItensPerPage = itensDto.Count, TotalPages = 1, TotalItens = itensDto.Count };

                return await response.GenerateResponse(statusCode: HttpStatusCode.OK, collection: paginator, message: "Resultado obtido com sucesso.", count: paginator.Itens.Count);
            }
        }
    }
}
