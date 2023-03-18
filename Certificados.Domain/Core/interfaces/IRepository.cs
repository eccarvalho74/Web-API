using Certificados.Domain.Core.DTO;
using System.Linq.Expressions;

namespace Certificados.Domain.Core.Interfaces
{
    public interface IRepository<T>  where T : class
    {
        Task<List<T>> Get();
        Task<List<T>> Get(Expression<Func<T, bool>> predicate);
        Task<List<TDTO>> Get<TDTO>();
        Task<List<TDTO>> Get<TDTO>(Expression<Func<T, bool>> predicate);

        Task<T> GetById(int id);
        Task<TDTO> GetById<TDTO>(int id);

        Task<PagedListResponse<TDTO>> GetPaged<TDTO>(int page = 1, int pageSize = 10, bool countTotal = false);
        Task<PagedListResponse<TDTO>> GetPaged<TDTO>(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 10, bool countTotal = false);

        Task<T> Insert(T entity);
        Task<List<T>> InsertRange(List<T> entity);

        Task<T> Update(T entity);
        Task<List<T>> Update(List<T> entities);
       
        Task<T> Disable(T entity);

        Task Delete(object id);
        Task DeleteRange(int[] ids);
       
        Task<T> Clone(T entity);

        Task Save();
    }
}
