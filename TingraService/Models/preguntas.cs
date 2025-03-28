using TingraService.Models.Base;

namespace TingraService.Models
{
    public class Pregunta : BaseEntity
    {
        public string Preguntas { get; set; }
        public string? Descripcion {  get; set; }
        public Guid IdEmpresa { get; set; }
    }
}
