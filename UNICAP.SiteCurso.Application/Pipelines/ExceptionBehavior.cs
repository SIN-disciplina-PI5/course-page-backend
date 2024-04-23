using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Exceptions;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Domain.Exceptions;

namespace UNICAP.SiteCurso.Application.Pipelines
{
    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : Response
    {
        private readonly IEnumerable<IExceptionHandler<Response>> exceptionHandlers;
        private readonly Response response;
        private readonly IWebHostEnvironment environment;
        private readonly IHttpContextAccessor httpAccessor;

        public ExceptionBehavior(IEnumerable<IExceptionHandler<Response>> exceptionHandlers, Response response,
            IHttpContextAccessor httpAccessor, IWebHostEnvironment environment)
        {
            this.exceptionHandlers = exceptionHandlers;
            this.response = response;
            this.environment = environment;
            this.httpAccessor = httpAccessor;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                var _response = await exception.GetExceptionHandlerFromActualExceptionType(exceptionHandlers, response) as TResponse;

                var statusCode = MapExceptionToStatusCode(exception);
                httpAccessor.HttpContext.Response.StatusCode = (int)statusCode;

                return _response;

            }

        }
        private static HttpStatusCode MapExceptionToStatusCode(Exception exception)
        {
            return exception switch
            {
                BadRequestException _ => HttpStatusCode.BadRequest,
                UnauthorizedException _ => HttpStatusCode.Unauthorized,
                ForbiddenException _ => HttpStatusCode.Forbidden,
                NotFoundException _ => HttpStatusCode.NotFound,
                InternalServerErrorException _ => HttpStatusCode.InternalServerError,
                ProcessException _ => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
