using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Interfaces;

namespace UNICAP.SiteCurso.Application.Exceptions
{
    public static class Context
    {
        public static Task<Response> GetExceptionHandlerFromActualExceptionType(this Exception e, IEnumerable<IExceptionHandler<Response>> exceptionHandlers, Response response)
        {
            var actualHandler = exceptionHandlers.Where(q => q.GetType().Name.Contains(e.GetType().Name)).FirstOrDefault();
            if (actualHandler != null)
            {
                return actualHandler.Handle(e);
            }
            return CreateDefaultException(response, e == null ? null : e);
        }

        private static Task<Response> CreateDefaultException(Response response, Exception e = null)
        {
            return response.GenerateResponse(hasError: true, message: e.Message);
        }
    }
}
