using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Exceptions.Db.Register;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;
using UNICAP.SiteCurso.Domain.Entities;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response>
    {
        private readonly IMediator mediator;
        private readonly Response response;
        private readonly IEFContext eFContext;
        private readonly IMapper mapper;

        public CreateUserHandler(IMediator mediator, Response response, IEFContext eFContext, IMapper mapper)
        {
            this.mediator = mediator;
            this.response = response;
            this.eFContext = eFContext;
            this.mapper = mapper;
        }

        public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var emailExiste = await eFContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(request.Email.ToLower().Trim()));
            if(emailExiste is not null)
            {
                throw new Exception("Email já cadastrado no sistema!");
            }

            var senhaPadrao = GerarSenhaAleatoria();
            var loginPadrao = request.Email.ToLower().Trim();

            var user = mapper.Map<User>(request);
            user.Credentials.Password = senhaPadrao;
            user.Credentials.Login = loginPadrao;

            var responseData = new
            {
                Login = loginPadrao,
                Password = senhaPadrao
            };

            await SaveUserAsync(user, request);
            return await response.GenerateResponse(hasError: false, statusCode: HttpStatusCode.OK, message: Messages.RegisterSuccessMessage, collection: responseData, count: 1);

        }

        private static string GerarSenhaAleatoria()
        {
            var senhaPadrao = "Unicap@" + DateTime.Now.Year;

            return senhaPadrao;
        }

        private async Task SaveUserAsync(User user, CreateUserCommand request)
        {
            using (var tr = eFContext.BeginTransaction())
            {
                try
                {
                    await eFContext.Users.AddAsync(user);
                    var result = await eFContext.SaveChangesAsync();
                    if (result <= 0)
                    {
                        response.AddErrorMessages(message: Messages.PersistenceDbExceptionMessage);
                        throw new PersistanceDbException<CreateUserCommand>(request, nameof(CreateUserHandler));
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
