using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNICAP.SiteCurso.Application.DTOs
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
