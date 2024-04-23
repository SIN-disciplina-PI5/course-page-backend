using System;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.Exceptions.Db.Register
{
    public class PersistanceDbException<T> : Exception
    {
        public T Request { get; set; }
        public string HandlerName { get; set; }
        public PersistanceDbException(T request, string handlerName) : base(Messages.RegisterDbExceptionMessage)
        {
            Request = request;
            HandlerName = handlerName;
        }
    }
}
