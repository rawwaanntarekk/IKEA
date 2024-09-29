using LinkDev.IKEA.DAL.Models;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistance.Repositotries._Generic
{
    public class GenericRepositories<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;


        public GenericRepositories(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T?> GetAsync(int id)
        {
            var entity = _dbContext.Set<T>().FindAsync(id);

            if (entity is { })
                return await entity;
            return null;

        }

        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return await _dbContext.Set<T>().Where(e => e.IsDeleted == false).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(e => e.IsDeleted == false).ToListAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public IEnumerable<T> GetIEnumerable()
        {
            return _dbContext.Set<T>();
        }
        public void Add(T entity) 
            => _dbContext.Set<T>().Add(entity);
        public void Update(T entity)
            => _dbContext.Set<T>().Update(entity);
        public void Delete(T entity)
        {
            if (entity is { })
            {
                entity.IsDeleted = true;
                _dbContext.Set<T>().Update(entity);
            }
                
        }


    }
}

