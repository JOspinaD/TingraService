using TingraService.Common;

namespace TingraService.BLL.Errors
{
    public static class PreguntasErrors
    {
        public static readonly Error NotExists = new(
            "Pregunta.NotExists",
            "Pregunta no encontrado"
            );

        public static readonly Error AlreadyExists = new(
            ".AlreadyExists",
            "La Pregunta ya existe"
            );

        public static readonly Error Unhandled = new(
            "Pregunta.Unhandled",
            "Error no controlado"
            );

        public static readonly Error EmpresaNotExists = new(
            "Empresa.NotExists",
            "Empresa encontrado"
            );
    }
}
