using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Models.Pagination;
using Server.Repository.Interfaces;
using System.Linq.Expressions;

namespace Server.Repository.Classes
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<PagedList<T>> GetPaged(PaginationParammeters paginationParammeters, IQueryable<T> query)
        {
            return await PagedList<T>.ToPagedList(query, paginationParammeters.PageNumber, paginationParammeters.PageSize);
        }

        public void Upadate(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
