using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNICAP.SiteCurso.Domain.Entities
{
    public class BaseEntity<Tid>
    {
        [Column("ID")]
        [Required]
        public Tid Id { get; set; }

        [Column("UPDATED_AT")]
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        [Column("CREATED_AT")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("IS_ACTIVE")]
        [Required]
        public bool IsActive { get; set; }
    }
}
