using System;

namespace UNICAP.SiteCurso.Application.DTOs.BaseFolder
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
