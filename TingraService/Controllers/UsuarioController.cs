using Microsoft.AspNetCore.Mvc;
using TingraService.BLL.Services.Contract;
using TingraService.Controllers.Extensions;
using TingraService.DTO.Usuario;

namespace TingraService.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController(IUsuarioService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioReadDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioReadDto>> Post(UsuarioWriteDto usuarioWriteDto)
        {
            var result = await _service.CreateAsync(usuarioWriteDto);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioReadDto>> Put(Guid id, UsuarioWriteDto usuarioWriteDto)
        {
            var result = await _service.UpdateAsync(id, usuarioWriteDto);
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
