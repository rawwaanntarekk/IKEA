using LinkDev.IKEA.DAL.Models;
using LinkDev.IKEA.DAL.Models.Employees;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositotries._Generic
{
    public class GenericRepositories<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;


        public GenericRepositories(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T? Get(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);

            if (entity is { })
                return entity;
            return null;

        }

        public IEnumerable<T> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return _dbContext.Set<T>().AsNoTracking().ToList();

            return _dbContext.Set<T>().ToList();
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(int id)
        {
            var entityToDelete = _dbContext.Set<T>().Find(id);
            if (entityToDelete is { })
                _dbContext.Set<T>().Remove(entityToDelete);

            return _dbContext.SaveChanges();
        }


    }
}

