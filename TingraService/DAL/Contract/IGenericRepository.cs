using System.Linq.Expressions;

namespace TingraService.DAL.Contract
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<IQueryable<TModel>> GetAll();
        Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>> filter);
        Task<TModel> GetBy(Expression<Func<TModel, bool>> filter);
        Task<TModel> Add(TModel modelo);
        Task<TModel> Update(TModel modelo);
        Task<bool> Delete(TModel modelo);
        Task<int> Count();
        Task<int> Count(Expression<Func<TModel, bool>> filter);
    }
}
