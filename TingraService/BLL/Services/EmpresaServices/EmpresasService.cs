using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TingraService.BLL.Errors;
using TingraService.BLL.Services.Contract;
using TingraService.Common;
using TingraService.DAL.Contract;
using TingraService.DTO.Empresa;
using TingraService.Models;

namespace TingraService.BLL.Services.EmpresaServices
{
    public class EmpresaService(IGenericRepository<Empresa> repository, IMapper mapper) : IEmpresaService 

    {
        private readonly IGenericRepository<Empresa> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<EmpresaReadDto>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAll();
                return Result.Success(_mapper.Map<IEnumerable<EmpresaReadDto>>(result.Include(r => r.Pregunta)));
            }
            catch (Exception ex)
            { 
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<IEnumerable<EmpresaReadDto>>(EmpresasErrors.Unhandled);
            }
        }

        public async Task<Result<EmpresaReadDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _repository.GetBy(t => t.Id == id);
                if (result == null)
                {
                    return Result.Failure<EmpresaReadDto>(EmpresasErrors.NotExists);
                }
                return Result.Success(_mapper.Map<EmpresaReadDto>(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<EmpresaReadDto>(EmpresasErrors.Unhandled);
            }
        }

        public async Task<int> CountAsync(Expression<Func<Empresa, bool>> filter)
        {
            try
            {
                return await _repository.Count(filter);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("---> ERROR: " + ex.Message);
                return 0;
            }
        }
            
        public async Task<Result<EmpresaReadDto>> CreateAsync(EmpresaWriteDto empresaWriteDto)
        {
            try
            {
                var model = _mapper.Map<Empresa>(empresaWriteDto);
                var result = await _repository.Add(model);
                return Result.Success(_mapper.Map<EmpresaReadDto>(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<EmpresaReadDto>(EmpresasErrors.Unhandled);
            }
        }

        public async Task<Result<EmpresaReadDto>> UpdateAsync(Guid id, EmpresaWriteDto empresaWriteDto)
        {
            try
            {
                var model = await _repository.GetBy(t => t.Id == id);
                if (model == null)
                {
                    return Result.Failure<EmpresaReadDto>(EmpresasErrors.NotExists);
                }

                _mapper.Map(empresaWriteDto, model);
                var result = await _repository.Update(model);
                return Result.Success(_mapper.Map<EmpresaReadDto>(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<EmpresaReadDto>(EmpresasErrors.Unhandled);
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var model = await _repository.GetBy(t => t.Id == id);
                if (model == null)
                {
                    return EmpresasErrors.NotExists;
                }
                await _repository.Delete(model);
                return Result.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return EmpresasErrors.Unhandled;
            }
        }
    }
}
