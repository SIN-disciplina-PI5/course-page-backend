using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.Pipelines
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        private readonly Response response;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, Response response)
        {
            this.validators = validators;
            this.response = response;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var failures = validators
                               .Select(x => x.Validate(request))
                               .SelectMany(x => x.Errors)
                               .Where(x => x != null)
                               .ToList();


            return failures.Any() ? await Errors(failures, response) : await next();
        }

        private Task<TResponse> Errors(List<ValidationFailure> failures, Response _response)
        {
            failures.ForEach(e =>
            {
                _response.AddErrorMessages(e.ErrorMessage);
            });
            _response.SetStatusCode(_response.StatusCode != 0 ? _response.StatusCode : HttpStatusCode.BadRequest);

            return Task.FromResult(_response as TResponse);
        }
    }
}
