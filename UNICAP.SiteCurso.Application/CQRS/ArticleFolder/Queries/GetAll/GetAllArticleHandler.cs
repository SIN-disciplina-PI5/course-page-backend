using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

            var list = eFContext.Articles.Where(x => request.withDisabled ? true : x.IsActive).OrderBy(x => x.UpdatedAt).ToList();
            var articleDTO = mapper.Map<List<ArticleDTO>>(list);

            return await response.GenerateResponse(statusCode: HttpStatusCode.OK, collection: articleDTO, message: "Resultado obtido com sucesso.", count: articleDTO.Count);


        }
    }
}
