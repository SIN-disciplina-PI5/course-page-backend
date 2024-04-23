using System;
using UNICAP.SiteCurso.Domain.Entities;
using UNICAP.SiteCurso.Domain.Enum;

namespace UNICAP.SiteCurso.Application.DTOs.UserFolder
{
    public class UserCredentialsDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string RoleDescription { get; set; }
        public UserCredentials Credentials { get; set; }
    }
}
