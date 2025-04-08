using System.Linq.Expressions;
using TingraService.Common;
using TingraService.DTO.Empresa;
using TingraService.DTO.Pregunta;
using TingraService.Models;

namespace TingraService.BLL.Services.Contract
{
    public interface IPreguntaService
    {
        Task<Result<IEnumerable<EmpresaReadDto>>> GetAllAsync();
        Task<Result<IEnumerable<PreguntaReadDto>>> GetAllAsync(Expression<Func<Pregunta, bool>> filter);
        Task<Result<PreguntaReadDto>> GetByIdAsync(Guid id);
        Task<int> CountAsync(Expression<Func<Pregunta, bool>> Filter);
        Task<Result<PreguntaReadDto>> CreateAsync(PreguntaWriteDto preguntaWriteDto);
        Task<Result<PreguntaReadDto>> UpdateAsync(Guid id, PreguntaWriteDto preguntaWriteDto);
        Task<Result> DeleteAsync(Guid id);
    }
}
