using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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
            var articleDTO = request.WithDisabled ? mapper.Map<List<ArticleDTO>>(await eFContext.Articles.ToListAsync()) :
                mapper.Map<List<ArticleDTO>>(await eFContext.Articles.Where(e => e.IsActive == true).ToListAsync());

            if (articleDTO is null)
            {
                response.AddErrorMessages(message: Messages.RegisterNotFoundMessage);
                return await response.GenerateResponse(statusCode: HttpStatusCode.BadRequest,
                    hasError: true,
                    message: Messages.RegisterNotFoundMessage);
            }

            return await response.GenerateResponse(statusCode: HttpStatusCode.OK,
                message: Messages.GetRegisterSuccess,
                collection: articleDTO,
                count: articleDTO.Count());
        }
    }
}
