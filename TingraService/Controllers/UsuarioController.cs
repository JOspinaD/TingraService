using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TingraService.BLL.Services.Contract;
using TingraService.Controllers.Extensions;
using TingraService.DTO.Usuario;
using TingraService.Models;

namespace TingraService.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController(IUsuarioService _service, IConfiguration configuration) : ControllerBase
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Datos inválidos");

            var result = await _service.LoginAsync(loginDto);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblemDetails();
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
