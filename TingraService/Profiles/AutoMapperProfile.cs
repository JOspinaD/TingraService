using TingraService.DTO.Usuario;
using TingraService.DTO.Empresa;
using TingraService.DTO.Pregunta; 
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

            CreateMap<Empresa, EmpresaReadDto>();
            CreateMap<EmpresaWriteDto, Empresa>();

            CreateMap<Pregunta, PreguntaReadDto>();
            CreateMap<PreguntaWriteDto, Pregunta>();

        }

    }
}
