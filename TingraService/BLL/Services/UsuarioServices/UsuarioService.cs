using AutoMapper;
using TingraService.BLL.Errors;
using TingraService.Common;
using TingraService.DAL.Contract;
using TingraService.DTO.Usuario;
using TingraService.Models;

namespace TingraService.BLL.Services.UsuarioServices
{
    public class UsuarioService(IGenericRepository<Usuario> repository, IMapper mapper, PasswordService passwordService)
    {
        private readonly IGenericRepository<Usuario> _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly PasswordService _passwordService;

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
