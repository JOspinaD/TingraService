using AutoMapper;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public async Task<Result<TokenResponseDto>> LoginAsync(UsuarioWriteDto usuarioWriteDto)
        {
            try
            {
                var usuario = await _repository.GetBy(t => t.Correo == usuarioWriteDto.Correo);

                if (usuario == null)
                    return Result.Failure<TokenResponseDto>(UsuarioErrors.NotExists);

                var isValid = _passwordService.verifyPassword(
                    usuarioWriteDto.HashContraseña,
                    usuario.HashContraseña,
                    usuario.Salt
                );

                if (!isValid)
                    return Result.Failure<TokenResponseDto>(UsuarioErrors.InvalidPassword);

                var response = new TokenResponseDto
                {
                    AccessToken = CreateToken(usuario),
                    RefreshToken = await GenerateAndSaveRefreshTokenAsync(usuario)
                };



                return Result.Success(response);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("--> ERROR: ", ex.Message);
                return Result.Failure<TokenResponseDto>(UsuarioErrors.Unhandled);
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(Usuario usuario)
        {
            var refreshToken = GenerateRefreshToken();
            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _repository.Update(usuario);
            return refreshToken;
        }

        private string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Correo )
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
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
