using TingraService.DTO.Base;

namespace TingraService.DTO.Pregunta
{
    public class PreguntaReadDto : BaseReadDto
    {
        public string Texto { get; set; }
        public string Descripcion { get; set; }
        public Guid? IdEmpresa { get; set; }
    }
}
