using LinkDev.IKEA.DAL.Models;
using LinkDev.IKEA.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositotries._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        public T? Get(int id);
        public IEnumerable<T> GetAll(bool withAsNoTracking = true);
        public IQueryable<T> GetAllAsIQueryable();
        public int Add(T entity);
        public int Update(T entity);
        public int Delete(T entity);
    }
}
