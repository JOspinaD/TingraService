namespace TingraService.DTO.Usuario
{
    public class LoginDto
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }

    public class LoginResponseDto
    {
        public string Correo { get; set; }
        public string Token { get; set; }
    }

}
