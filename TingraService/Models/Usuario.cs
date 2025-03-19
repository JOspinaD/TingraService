using TingraService.Models.Base;

namespace TingraService.Models
{
    public class Usuario : BaseEntity
    {
        public string Nombre { get; set; }   
        public string Correo { get; set; }
        public string HashContraseña { get; set; }
        public string? Salt { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}
