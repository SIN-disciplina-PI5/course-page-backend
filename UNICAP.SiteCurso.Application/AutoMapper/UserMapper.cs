using AutoMapper;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Create;
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

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.Credentials.Role, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.CredentialsId, opt => opt.Ignore())
                .ForMember(dest => dest.Artigos, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
