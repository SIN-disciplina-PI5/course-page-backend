using System;
using UNICAP.SiteCurso.Domain.Constants;

namespace UNICAP.SiteCurso.Application.Exceptions.Db.Remove
{
    public class RemoveDbException : Exception
    {
        public string HandlerName { get; private set; }
        public int Id { get; private set; }
        public RemoveDbException(int Id, string handlerName) : base(Messages.RemoveDbExceptionMessage)
        {
            this.Id = Id;
            HandlerName = handlerName;
        }
    }
}
