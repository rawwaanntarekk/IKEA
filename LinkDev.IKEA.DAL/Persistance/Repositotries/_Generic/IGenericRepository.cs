using LinkDev.IKEA.DAL.Models;
using LinkDev.IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositotries._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        // An asynchronous operation that can return a value
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);
        IQueryable<T> GetIQueryable();
        IEnumerable<T> GetIEnumerable();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
