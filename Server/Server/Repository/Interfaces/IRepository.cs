using Server.Models.Pagination;
using System.Linq.Expressions;

namespace Server.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();

        Task<PagedList<T>> GetPaged(PaginationParammeters paginationParammeters, IQueryable<T> query);

        Task<T> GetById(Expression<Func<T, bool>> predicate);
            
        void Upadate(T entity);

        void Add(T entity);
        
        void Delete(T entity);
    }
}
