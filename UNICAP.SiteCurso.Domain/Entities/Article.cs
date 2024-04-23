using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UNICAP.SiteCurso.Domain.Entities
{
    [Table("TB_ARTICLE")]
    public class Article : BaseEntity<int>
    {
        [Column("TITULO")]
        [Required]
        [MaxLength(90)]
        [MinLength(3)]
        public string Titulo { get; set; }

        [Column("DESCRICAO")]
        [Required]
        [MaxLength(3000)]
        [MinLength(20)]
        public string Descricao { get; set; }

        [Column("USER_ID")]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User Usuario { get; set; }


    }
}
