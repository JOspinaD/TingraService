using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TingraService.DAL.Contract;

namespace TingraService.DAL
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TModel> Add(TModel modelo)
        {
            try
            {
                _context.Set<TModel>().Add(modelo);
                await _context.SaveChangesAsync();
                return modelo;
            }
            catch { throw; }
        }

        public async Task<int> Count()
        {
            try
            {
                return _context.Set<TModel>().Count();
            }
            catch { throw; }
        }

        public async Task<int> Count(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                return _context.Set<TModel>().Count(filter);
            }
            catch { throw; }
            ;
        }

        public async Task<bool> Delete(TModel modelo)
        {
            try
            {
                _context.Set<TModel>().Remove(modelo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
            ;
        }

        public async Task<IQueryable<TModel>> GetAll()
        {
            try
            {
                IQueryable<TModel> queryModel = _context.Set<TModel>();
                return queryModel;
            }
            catch { throw; }
            ;
        }

        public async Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                IQueryable<TModel> queryModel = _context.Set<TModel>().Where(filter);
                return queryModel;
            }
            catch { throw; }
            ;
        }

        public async Task<TModel> GetBy(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel modelo = await _context.Set<TModel>().FirstOrDefaultAsync(filter);
                return modelo;
            }
            catch { throw; }
            ;
        }

        public async Task<TModel> Update(TModel modelo)
        {
            try
            {
                _context.Set<TModel>().Update(modelo);
                await _context.SaveChangesAsync();
                return modelo;
            }
            catch { throw; }
            ;
        }
    }
}
