using AutoMapper;
using UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Create;
using UNICAP.SiteCurso.Application.DTOs.ArticleFolder;
using UNICAP.SiteCurso.Domain.Entities;

namespace UNICAP.SiteCurso.Application.AutoMapper
{
    public class ArticleMapper : Profile
    {
        public ArticleMapper() 
        {
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<CreateArticleCommand, Article>()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Usuario, opt => opt.Ignore());
        }
    }
}
