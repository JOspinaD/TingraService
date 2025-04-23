using TingraService.Models.Base;

namespace TingraService.Models
{
    public class Empresa : BaseEntity
    {
        public string Nombre { get; set; }
        public string Dirreccion { get; set; }
        public string? Telefono{ get; set; }
        public string? Email { get; set; }   
        public virtual ICollection<Pregunta>? Pregunta { get; set; }
    }
}

