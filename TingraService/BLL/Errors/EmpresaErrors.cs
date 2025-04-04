using TingraService.Common;

namespace TingraService.BLL.Errors
{
    public static class EmpresasErrors
    {
        public static readonly Error NotExists = new(
            "Empresa.NotExists",
            "Empresa no encontrado"
            );

        public static readonly Error AlreadyExists = new(
            ".AlreadyExists",
            "La empresa ya existe"
            );

        public static readonly Error Unhandled = new(
            "Empresa.Unhandled",
            "Error no controlado"
            );

        public static readonly Error PreguntaNotExists = new(
            "Pregunta.NotExists",
            "Pregunta encontrado"
            );
    }
}
