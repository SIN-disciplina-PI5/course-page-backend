using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.DTOs.UserFolder;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetAll
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUserQuery, Response>
    {
        private readonly IEFContext eFContext;
        private readonly IMapper mapper;
        private readonly Response response;

        public GetAllUsersHandler(IEFContext eFContext, IMapper mapper, Response response)
        {
            this.eFContext = eFContext;
            this.mapper = mapper;
            this.response = response;
        }

        public async Task<Response> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var usersDTO = request.WithDisabled ? mapper.Map<List<UserDTO>>(await eFContext.Users.ToListAsync()) :
                mapper.Map<List<UserDTO>>(await eFContext.Users.Where(e => e.IsActive == true).ToListAsync());

            if (usersDTO is null)
            {
                response.AddErrorMessages(message: Messages.RegisterNotFoundMessage);
                return await response.GenerateResponse(statusCode: HttpStatusCode.BadRequest,
                    hasError: true,
                    message: Messages.RegisterNotFoundMessage);
            }

            return await response.GenerateResponse(statusCode: HttpStatusCode.OK,
                message: Messages.GetRegisterSuccess,
                collection: usersDTO,
                count: usersDTO.Count());
        }
    }
}
