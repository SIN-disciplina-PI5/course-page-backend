using System;

namespace UNICAP.SiteCurso.Application.CQRS.BaseFolder
{
    public class UpdateBaseCommand
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get { return DateTime.Now; } }
    }
}
