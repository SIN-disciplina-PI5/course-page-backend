using System.ComponentModel;

namespace UNICAP.SiteCurso.Infrastructure.Enum
{
    public enum Role
    {
        [Description("Administrador")]
        Admin = 1,

        [Description("Aluno")]
        Student = 2,

        [Description("Professor")]
        Teacher = 3,
    }
}
