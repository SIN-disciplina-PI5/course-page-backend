using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.DTOs.UserFolder;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response>
    {
        private readonly IEFContext eFContext;
        private readonly Response response;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpAccessor;

        public GetUserByIdHandler(IEFContext eFContext, Response response, IMapper mapper, IHttpContextAccessor httpAccessor)
        {
            this.eFContext = eFContext;
            this.response = response;
            this.mapper = mapper;
            this.httpAccessor = httpAccessor;
        }

        public async Task<Response> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await eFContext.Users.FirstOrDefaultAsync(p => p.Id.Equals(request.Id));
            if (user is null)
            {
                response.AddErrorMessages(message: Messages.RegisterNotFoundMessage);
                return await response.GenerateResponse(statusCode: HttpStatusCode.BadRequest, hasError: true, message: Messages.RegisterNotFoundMessage);
            }

            var userDTO = mapper.Map<UserDTO>(user);
            return await response.GenerateResponse(statusCode: HttpStatusCode.OK,
                message: Messages.GetRegisterSuccess,
                collection: userDTO,
                count: 1);
        }
    }
}
