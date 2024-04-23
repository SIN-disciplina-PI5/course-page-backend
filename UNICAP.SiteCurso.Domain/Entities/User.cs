using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNICAP.SiteCurso.Domain.Entities
{
    [Table("TB_USER")]
    public class User : BaseEntity<int>
    {
        [Column("NOME")]
        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string Nome { get; set; }

        [Column("EMAIL")]
        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string Email { get; set; }

        [Column("CREDENTIALS_ID")]
        [Required]
        [ForeignKey("Credentials")]
        public int CredentialsId { get; set; }
        public virtual UserCredentials Credentials { get; set; }

        public virtual ICollection<Article> Artigos { get; set; }
    }
}
