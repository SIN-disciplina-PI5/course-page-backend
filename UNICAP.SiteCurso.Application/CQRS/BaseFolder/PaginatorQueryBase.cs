namespace UNICAP.SiteCurso.Application.CQRS.BaseFolder
{
    public class PaginatorQueryBase
    {
        public bool WithPagination { get; set; }
        public bool WithDisabled { get; set; }
        public int CurrentPage { get; set; }
        public int ItensPerPage { get; set; }
    }
}
