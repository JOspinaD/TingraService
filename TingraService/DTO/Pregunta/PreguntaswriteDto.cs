namespace TingraService.DTO.Pregunta
{
    public class PreguntaWriteDto
    {
        public string Texto { get; set; }
        public string Descripcion { get; set; }
        public Guid? IdEmpresa { get; set; }
    }
}
