using UNICAP.SiteCurso.Application.DTOs.BaseFolder;
using UNICAP.SiteCurso.Domain.Enum;

namespace UNICAP.SiteCurso.Application.DTOs.UserFolder
{
    public class UserDTO : BaseDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public Role Cargo { get; set; }
        public string DescricaoCargo { get; set; }
    }
}
