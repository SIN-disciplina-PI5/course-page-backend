using AutoMapper;
using UNICAP.SiteCurso.Application.DTOs.UserFolder;
using UNICAP.SiteCurso.Application.Extensions;
using UNICAP.SiteCurso.Domain.Entities;

namespace UNICAP.SiteCurso.Application.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Credentials.Role))
                .ForMember(dest => dest.DescricaoCargo, opt => opt.MapFrom(src => src.Credentials.Role.GetDescription()));
        }
    }
}
