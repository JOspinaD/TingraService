using TingraService.Common;

namespace TingraService.BLL.Errors
{
    public class UsuarioErrors
    {
        public static readonly Error NotExists = new(
            "Usuario.NotExists",
            "Usuario no encontrado"
            );

        public static readonly Error AlreadyExists = new(
            "Usuario.AlreadyExists",
            "El usuario ya existe"
            );

        public static readonly Error Unhandled = new(
            "Usuario.Unhandled",
            "Error no controlado"
            );

        public static readonly Error InvalidPassword = new(
            "Usuario.InvalidCredentials",
            "Credenciales invalidas"
            );

        public static readonly Error InvalidRefreshToken = new(
            "Usuario.InvalidRefreshToken",
            "El refresh token es inválido o ha expirado"
            );
    }
}
