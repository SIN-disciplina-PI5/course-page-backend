using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.ArticleFolder;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Queries.GetById
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IMapper mapper;
        private readonly Response response;
        private readonly IHttpContextAccessor httpAccessor;

        public GetArticleByIdHandler(IEFContext eFContext, IMapper mapper, Response response, IHttpContextAccessor httpAccessor)
        {
            this.eFContext = eFContext;
            this.mapper = mapper;
            this.response = response;
            this.httpAccessor = httpAccessor;
        }

        public async Task<Response> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await eFContext.Articles.FirstOrDefaultAsync(p => p.Id.Equals(request.Id));
            if (article is null)
            {
                response.AddErrorMessages(message: Messages.RegisterNotFoundMessage);
                return await response.GenerateResponse(statusCode: HttpStatusCode.BadRequest, hasError: true, message: Messages.RegisterNotFoundMessage);
            }

            var articleDTO = mapper.Map<ArticleDTO>(article);
            return await response.GenerateResponse(statusCode: HttpStatusCode.OK,
                message: Messages.GetRegisterSuccess,
                collection: articleDTO,
                count: 1);
        }
    }
}
