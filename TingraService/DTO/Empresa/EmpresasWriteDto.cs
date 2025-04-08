namespace TingraService.DTO.Empresa
{
    public class EmpresaWriteDto
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Guid? IdPregunta { get; set; }
    }
}
