using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Exceptions.Db.Register;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;
using UNICAP.SiteCurso.Domain.Entities;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Create
{
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Response>
    {
        private readonly IMediator mediator;
        private readonly IEFContext eFContext;
        private readonly Response response;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpAccessor;

        public CreateArticleHandler(IMediator mediator, IEFContext eFContext, Response response, IMapper mapper, IHttpContextAccessor httpAccessor)
        {
            this.mediator = mediator;
            this.eFContext = eFContext;
            this.response = response;
            this.mapper = mapper;
            this.httpAccessor = httpAccessor;
        }

        public async Task<Response> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var artcile = mapper.Map<Article>(request);

            await SaveArticleAsync(artcile, request);
            return await response.GenerateResponse(hasError: false, 
                statusCode: HttpStatusCode.OK, 
                message: Messages.RegisterSuccessMessage, 
                count: 1);
        }

        private async Task SaveArticleAsync(Article article, CreateArticleCommand request)
        {
            using (var tr = eFContext.BeginTransaction())
            {
                try
                {
                    await eFContext.Articles.AddAsync(article);
                    var result = await eFContext.SaveChangesAsync();
                    if (result <= 0)
                    {
                        response.AddErrorMessages(message: Messages.PersistenceDbExceptionMessage);
                        throw new PersistanceDbException<CreateArticleCommand>(request, nameof(CreateArticleHandler));
                    }
                    tr.Commit();

                }
                catch (Exception e)
                {
                    await tr.RollbackAsync();
                    throw e.InnerException ?? e;
                }

            }
        }
    }
}
