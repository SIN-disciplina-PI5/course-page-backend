using System;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.Exceptions.Db.Update
{
    public class UpdateDbException<T> : Exception
    {
        public T Request { get; set; }
        public string HandlerName { get; set; }

        public UpdateDbException(T request, string handlerName) : base(Messages.UpdateDbExceptionMessage)
        {
            Request = request;
            HandlerName = handlerName;
        }
    }
}
