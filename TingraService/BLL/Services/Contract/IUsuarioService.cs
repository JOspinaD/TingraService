using TingraService.Common;
using TingraService.DTO.Usuario;
using TingraService.Models;

namespace TingraService.BLL.Services.Contract
{
    public interface IUsuarioService
    {
        Task<Result<IEnumerable<UsuarioReadDto>>> GetAllAsync();
        Task<Result<UsuarioReadDto>> GetByIdAsync(Guid id);
        Task<Result<UsuarioReadDto>> CreateAsync(UsuarioWriteDto usuarioCreateDto);
        Task<Result<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<Result<UsuarioReadDto>> UpdateAsync(Guid id, UsuarioWriteDto usuarioUpdateDto);
        Task<Result> DeleteAsync(Guid id);
    }
}
