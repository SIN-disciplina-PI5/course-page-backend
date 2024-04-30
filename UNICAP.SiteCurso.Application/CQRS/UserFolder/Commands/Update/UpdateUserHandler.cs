using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Exceptions.Db.Update;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IMapper mapper;
        private readonly Response response;
        private readonly IMediator mediator;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpAccessor;

        public UpdateUserHandler(IEFContext eFContext, IMapper mapper, Response response, IMediator mediator, IConfiguration configuration, IHttpContextAccessor httpAccessor)
        {
            this.eFContext = eFContext;
            this.mapper = mapper;
            this.response = response;
            this.mediator = mediator;
            this.configuration = configuration;
            this.httpAccessor = httpAccessor;
        }

        public async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using (var tr = eFContext.BeginTransaction())
            {
                try
                {
                    var user = await eFContext.Users.Include(e => e.Credentials).FirstOrDefaultAsync(e => e.Id.Equals(request.Id));
                    if (user is null)
                    {
                        response.AddErrorMessages(message: Messages.RegisterNotFoundMessage);
                        return
                            await response.GenerateResponse(
                            statusCode: HttpStatusCode.BadRequest,
                            hasError: true,
                            message: Messages.RegisterNotFoundMessage);
                    }
                    mapper.Map(request, user);
                    var result = await eFContext.SaveChangesAsync();

                    if (result <= 0)
                    {
                        response.AddErrorMessages(message: Messages.UpdateDbExceptionMessage);
                        throw new UpdateDbException<UpdateUserCommand>(request, nameof(UpdateUserHandler));
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
