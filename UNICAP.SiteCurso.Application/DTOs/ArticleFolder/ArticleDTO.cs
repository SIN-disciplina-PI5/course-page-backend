using UNICAP.SiteCurso.Application.DTOs.BaseFolder;

namespace UNICAP.SiteCurso.Application.DTOs.ArticleFolder
{
    public class ArticleDTO : BaseDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}
