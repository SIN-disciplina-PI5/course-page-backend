using System.Collections.Generic;

namespace UNICAP.SiteCurso.Application.DTOs
{
    public class Paginator<T>
    {
        public int CurrentPage { get; set; }
        public int ItensPerPage { get; set; }
        public int TotalItens { get; set; }
        public int TotalPages { get; set; }
        public bool IsLastPage { get { return CurrentPage == TotalPages; } }
        public List<T> Itens { get; set; }
    }
}
