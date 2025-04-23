using TingraService.Models.Base;

namespace TingraService.Models
{
    public class Pregunta : BaseEntity
    {
        public string Texto { get; set; }
        public string? Descripcion {  get; set; }
        public Guid IdEmpresa { get; set; }

        public virtual Empresa? Empresa { get; set; }
    }
}
