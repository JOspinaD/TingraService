using TingraService.DTO.Base;

namespace TingraService.DTO.Empresa
{
    public class EmpresaReadDto : BaseReadDto
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Guid? IdPregunta { get; set; }

    }
}
