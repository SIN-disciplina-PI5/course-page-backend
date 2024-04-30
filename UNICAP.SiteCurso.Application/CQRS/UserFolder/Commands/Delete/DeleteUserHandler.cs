using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Exceptions.Db.Remove;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IHttpContextAccessor httpContext;
        private readonly Response response;

        public DeleteUserHandler(IEFContext eFContext, IHttpContextAccessor httpContext, Response response)
        {
            this.eFContext = eFContext;
            this.httpContext = httpContext;
            this.response = response;
        }

        public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            using (var tr = eFContext.BeginTransaction())
            {
                try
                {
                    var user = await eFContext.Users.FirstOrDefaultAsync(p => p.Id.Equals(request.Id));

                    if (user is null)
                    {
                        response.AddErrorMessages(message: Messages.RemoveDbExceptionMessage);
                        return await response.GenerateResponse(statusCode: HttpStatusCode.BadRequest, hasError: true, message: Messages.RemoveDbExceptionMessage);
                    }

                    //user.IsActive = new Domain.ValueObjects.Active(false);
                    user.IsActive = false;
                    eFContext.Users.Update(user);
                    var result = await eFContext.SaveChangesAsync();
                    if (result <= 0)
                    {
                        response.AddErrorMessages(message: Messages.RemoveDbExceptionMessage);
                        throw new RemoveDbException(request.Id, nameof(DeleteUserHandler));

                    }

                    tr.Commit();
                    return await response.GenerateResponse(statusCode: HttpStatusCode.OK, hasError: false, message: Messages.DeleteUserSuccess);
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
