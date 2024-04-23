using System;

namespace UNICAP.SiteCurso.Application.CQRS.BaseFolder
{
    public class CreateBaseCommand
    {
        public DateTime CreatedAt { get { return DateTime.Now; } }
        public DateTime UpdatedAt { get { return DateTime.Now; } }
        public bool IsActive { get { return true; } }
        
    }
}
