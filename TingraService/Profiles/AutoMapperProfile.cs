using TingraService.DTO.Usuario;
using TingraService.Models;
using AutoMapper;

namespace TingraService.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioReadDto>();
            CreateMap<UsuarioWriteDto, Usuario>();
        }
    }
}
