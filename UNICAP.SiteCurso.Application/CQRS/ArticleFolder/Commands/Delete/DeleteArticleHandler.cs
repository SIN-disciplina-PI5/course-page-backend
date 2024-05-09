using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Delete;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Exceptions.Db.Remove;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Delete
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IHttpContextAccessor httpContext;
        private readonly Response response;

        public DeleteArticleHandler(IEFContext eFContext, IHttpContextAccessor httpContext, Response response)
        {
            this.eFContext = eFContext;
            this.httpContext = httpContext;
            this.response = response;
        }

        public async Task<Response> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            using (var tr = eFContext.BeginTransaction())
            {
                try
                {
                    var article = await eFContext.Articles.FirstOrDefaultAsync(p => p.Id.Equals(request.Id));

                    if (article is null)
                    {
                        response.AddErrorMessages(message: Messages.RemoveDbExceptionMessage);
                        return await response.GenerateResponse(statusCode: HttpStatusCode.BadRequest, hasError: true, message: Messages.RemoveDbExceptionMessage);
                    }

                    article.IsActive = false;
                    eFContext.Articles.Update(article);
                    var result = await eFContext.SaveChangesAsync();
                    if (result <= 0)
                    {
                        response.AddErrorMessages(message: Messages.RemoveDbExceptionMessage);
                        throw new RemoveDbException(request.Id, nameof(DeleteUserHandler));

                    }

                    tr.Commit();
                    return await response.GenerateResponse(statusCode: HttpStatusCode.OK, hasError: false, message: Messages.DeleteRegisterSuccess);
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
