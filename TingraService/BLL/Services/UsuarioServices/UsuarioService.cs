using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TingraService.BLL.Errors;
using TingraService.BLL.Services.Contract;
using TingraService.Common;
using TingraService.DAL.Contract;
using TingraService.DTO.Usuario;
using TingraService.Models;

namespace TingraService.BLL.Services.UsuarioServices
{
    public class UsuarioService(IGenericRepository<Usuario> repository, IMapper mapper, PasswordService passwordService, IConfiguration configuration) : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly PasswordService _passwordService = passwordService;

        public async Task<Result<UsuarioReadDto>> CreateAsync(UsuarioWriteDto usuarioWriteDto)
        {
            try
            {
                if (await _repository.Count(t => t.Nombre == usuarioWriteDto.Nombre) > 0)
                {
                    return Result.Failure<UsuarioReadDto>(UsuarioErrors.AlreadyExists);
                }

                var (hash, salt) = _passwordService.HashPassword(usuarioWriteDto.HashContraseña);
                var model = _mapper.Map<Usuario>(usuarioWriteDto);
                model.HashContraseña = hash;
                model.Salt = salt;
                var result = await _repository.Add(model);
                return Result.Success(_mapper.Map<UsuarioReadDto>(result));
            }
            catch
            {
                return Result.Failure<UsuarioReadDto>(UsuarioErrors.Unhandled);
            }

        }

        public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var usuario = await _repository.GetBy(t => t.Correo == loginDto.Correo);

                if (usuario == null)
                    return Result.Failure<LoginResponseDto>(UsuarioErrors.NotExists);

                var isValid = _passwordService.verifyPassword(
                    loginDto.Contraseña,
                    usuario.HashContraseña,
                    usuario.Salt
                );

                if (!isValid)
                    return Result.Failure<LoginResponseDto>(UsuarioErrors.InvalidPassword);
               
                var response = _mapper.Map<LoginResponseDto>(usuario);
               

                return Result.Success(response);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("--> ERROR: ", ex.Message);
                return Result.Failure<LoginResponseDto>(UsuarioErrors.Unhandled);
            }
        }

        private string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Correo )
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var model = await _repository.GetBy(t => t.Id == id);
                if (model == null)
                {
                    return UsuarioErrors.NotExists;
                }
                await _repository.Delete(model);
                return Result.Success();
            }
            catch
            {
                return UsuarioErrors.Unhandled;
            }
        }

        public async Task<Result<IEnumerable<UsuarioReadDto>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAll();
                return Result.Success(_mapper.Map<IEnumerable<UsuarioReadDto>>(result));
            }
            catch
            {
                return Result.Failure<IEnumerable<UsuarioReadDto>>(UsuarioErrors.Unhandled);
            }
        }

        public async Task<Result<UsuarioReadDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _repository.GetBy(t => t.Id == id);
                if (result == null)
                {
                    return Result.Failure<UsuarioReadDto>(UsuarioErrors.NotExists);
                }
                return Result.Success(_mapper.Map<UsuarioReadDto>(result));
            }
            catch
            {
                return Result.Failure<UsuarioReadDto>(UsuarioErrors.Unhandled);
            }
        }

        public async Task<Result<UsuarioReadDto>> UpdateAsync(Guid id, UsuarioWriteDto usuarioWriteDto)
        {
            try
            {
                var model = await _repository.GetBy(t => t.Id == id);
                if (model == null)
                {
                    return Result.Failure<UsuarioReadDto>(UsuarioErrors.NotExists);
                }

                _mapper.Map(usuarioWriteDto, model);

                if (!string.IsNullOrEmpty(usuarioWriteDto.HashContraseña))
                {
                    var (hash, salt) = _passwordService.HashPassword(usuarioWriteDto.HashContraseña);

                    model.HashContraseña = hash;
                    model.Salt = salt;
                }

                var result = await _repository.Update(model);
                return Result.Success(_mapper.Map<UsuarioReadDto>(result));
            }
            catch
            {
                return Result.Failure<UsuarioReadDto>(UsuarioErrors.Unhandled);
            }
        }

    }
}
