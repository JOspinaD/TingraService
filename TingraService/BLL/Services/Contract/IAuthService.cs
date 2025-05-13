using TingraService.Common;
using TingraService.Models;

namespace TingraService.BLL.Services.Contract
{
    public interface IAuthService
    {
        Task<Result<string>>AuthenticateAsync(string correo, string contrasena);
    }
}
