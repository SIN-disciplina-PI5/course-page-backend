using System;

namespace UNICAP.SiteCurso.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public object Collection { get; set; }

        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string message, object collection) : base(message)
        {
            Collection = collection;
        }
    }
}
