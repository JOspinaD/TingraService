using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using TingraService.BLL.Services.Contract;
using TingraService.Controllers.Extensions;
using TingraService.DTO.Empresa;
using TingraService.Models;

namespace TingraService.Controllers
{
    [Route("api/empresa")]
    [ApiController]
    public class EmpresaController(IEmpresaService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaReadDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaReadDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaReadDto>> Post(EmpresaWriteDto empresaWriteDto)
        {
            var result = await _service.CreateAsync(empresaWriteDto);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmpresaReadDto>> Put(Guid id, EmpresaWriteDto empresaWriteDto)
        {
            var result = await _service.UpdateAsync(id, empresaWriteDto);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.IsSuccess ? Ok() : result.ToProblemDetails();
        }
    }
}
