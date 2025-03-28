using System.Linq.Expressions;
using TingraService.Common;
using TingraService.DTO.Empresa;
using TingraService.Models;

namespace TingraService.BLL.Services.Contract
{
    public interface IEmpresaService
    {
        Task<Result<IEnumerable<EmpresaReadDto>>> GetAllAsync();
        Task<Result<EmpresaReadDto>> GetByIdAsync(Guid id);
        Task<int> CountAsync(Expression<Func<Empresa, bool>> filter);
        Task<Result<EmpresaReadDto>> CreateAsync(EmpresaWriteDto empresaWriteDto);
        Task<Result<EmpresaReadDto>> UpdateAsync(Guid id, EmpresaWriteDto empresaWriteDto);
        Task<Result> DeleteAsync(Guid id);
    }
}
