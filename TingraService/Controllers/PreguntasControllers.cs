using Microsoft.AspNetCore.Mvc;
using TingraService.BLL.Services.Contract;
using TingraService.Controllers.Extensions;
using TingraService.DTO.Pregunta;

namespace TingraService.Controllers
{
    [Route("api/pregunta")]
    [ApiController]
    public class PreguntaController(IPreguntaService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreguntaReadDto>>> GetAll(Guid? idEmpresa)
        {
            var result = await _service.GetAllAsync(r => !idEmpresa.HasValue|| r.IdEmpresa ==idEmpresa);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PreguntaReadDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPost]
        public async Task<ActionResult<PreguntaReadDto>> Post(PreguntaWriteDto preguntaWriteDto)
        {
            var result = await _service.CreateAsync(preguntaWriteDto);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PreguntaReadDto>> Put(Guid id, PreguntaWriteDto preguntaWriteDto)
        {
            var result = await _service.UpdateAsync(id, preguntaWriteDto);
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
