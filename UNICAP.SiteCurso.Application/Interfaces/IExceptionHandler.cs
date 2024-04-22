using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.Application.Interfaces
{
    public interface IExceptionHandler<TResponse>
    {
        Task<Response> Handle(Exception exception);
    }
}
