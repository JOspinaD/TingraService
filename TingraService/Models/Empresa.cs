using TingraService.Models.Base;

namespace TingraService.Models
{
    public class Empresa : BaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Propietario { get; set; }
        public DateOnly? FechaCreacion { get; set; }
        public string? Servicios { get; set; }
        public string? RedesSociales { get; set; }
        public bool PerteneceRedEmprendedores { get; set; }
        public bool RedEmprendedoresConfirmada { get; set; }
        public string? AspectosMejorar { get; set; }
        public string? Capacitadores { get; set; }
        public DateTime? FechaLlamada { get; set; }
        public string? Observaciones { get; set; }
        public string? Disponibilidad { get; set; }
        public bool CapacitacionRecibida { get; set; }
        public virtual ICollection<Pregunta>? Pregunta { get; set; }
    }
}

