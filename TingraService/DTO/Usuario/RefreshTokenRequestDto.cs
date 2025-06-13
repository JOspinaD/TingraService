namespace TingraService.DTO.Usuario
{
    public class RefreshTokenRequestDto
    {
        public Guid UsuarioId { get; set; }
        public required string RefreshToken { get; set; }

    }
}
