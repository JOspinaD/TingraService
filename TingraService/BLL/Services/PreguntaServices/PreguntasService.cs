using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TingraService.BLL.Errors;
using TingraService.BLL.Services.Contract;
using TingraService.Common;
using TingraService.DAL.Contract;
using TingraService.DTO.Pregunta;
using TingraService.Models;

namespace TingraService.BLL.Services.PreguntaServices
{
    public class PreguntaService(IGenericRepository<Pregunta> repository, IMapper mapper) : IPreguntaService
    {
        private readonly IGenericRepository<Pregunta> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<PreguntaReadDto>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAll();
                return Result.Success(_mapper.Map<IEnumerable<PreguntaReadDto>>(result));


            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<IEnumerable<PreguntaReadDto>>(PreguntasErrors.Unhandled);

            }
        }

        public async Task<Result<IEnumerable<PreguntaReadDto>>> GetAllAsync(Expression<Func<Pregunta, bool>> filter)
        {
            try
            {
                var result = await _repository.GetAll(filter);
                return Result.Success(_mapper.Map<IEnumerable<PreguntaReadDto>>(result));
            }
            catch (Exception ex) 
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<IEnumerable<PreguntaReadDto>>(PreguntasErrors.Unhandled);
            }
        }

        public async Task<int> CountAsync(Expression<Func<Pregunta, bool>> filter)
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

        public async Task<Result<PreguntaReadDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _repository.GetBy(t => t.Id == id);
                if (result == null)
                {
                    return Result.Failure<PreguntaReadDto>(PreguntasErrors.NotExists);
                }
                return Result.Success(_mapper.Map<PreguntaReadDto>(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<PreguntaReadDto>(PreguntasErrors.Unhandled);
            }
        }

        public async Task<Result<PreguntaReadDto>> CreateAsync(PreguntaWriteDto preguntaWriteDto)
        {
            try
            {
                var model = _mapper.Map<Pregunta>(preguntaWriteDto);
                var result = await _repository.Add(model);
                return Result.Success(_mapper.Map<PreguntaReadDto>(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<PreguntaReadDto>(PreguntasErrors.Unhandled);
            }
        }

        public async Task<Result<PreguntaReadDto>> UpdateAsync(Guid id, PreguntaWriteDto preguntaWriteDto)
        {
            try
            {
                var model = await _repository.GetBy(t => t.Id == id);
                if (model == null)
                {
                    return Result.Failure<PreguntaReadDto>(PreguntasErrors.NotExists);
                }

                _mapper.Map(preguntaWriteDto, model);
                var result = await _repository.Update(model);
                return Result.Success(_mapper.Map<PreguntaReadDto>(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return Result.Failure<PreguntaReadDto>(PreguntasErrors.Unhandled);
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var model = await _repository.GetBy(t => t.Id == id);
                if (model == null)
                {
                    return PreguntasErrors.NotExists;
                }
                await _repository.Delete(model);
                return Result.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine("---> ERROR: " + ex.Message);
                return PreguntasErrors.Unhandled;
            }
        }
    }
}
