using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Exceptions.Db.Update;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Update
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleCommand, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpAccessor;
        private readonly Response response;

        public UpdateArticleHandler(IEFContext eFContext, IMapper mapper, IMediator mediator, IHttpContextAccessor httpAccessor, Response response)
        {
            this.eFContext = eFContext;
            this.mapper = mapper;
            this.mediator = mediator;
            this.httpAccessor = httpAccessor;
            this.response = response;
        }

        public async Task<Response> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            using (var tr = eFContext.BeginTransaction())
            {
                try
                {
                    var article = await eFContext.Articles.FirstOrDefaultAsync(e => e.Id.Equals(request.Id));
                    if (article is null)
                    {
                        response.AddErrorMessages(message: Messages.RegisterNotFoundMessage);
                        return
                            await response.GenerateResponse(
                            statusCode: HttpStatusCode.BadRequest,
                            hasError: true,
                            message: Messages.RegisterNotFoundMessage);
                    }
                    mapper.Map(request, article);
                    var result = await eFContext.SaveChangesAsync();

                    if (result <= 0)
                    {
                        response.AddErrorMessages(message: Messages.UpdateDbExceptionMessage);
                        throw new UpdateDbException<UpdateArticleCommand>(request, nameof(UpdateArticleHandler));
                    }

                    tr.Commit();
                    return await response.GenerateResponse(HttpStatusCode.OK, false, Messages.UpdateSuccessMessage, count: 1);
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
