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
        public T? Get(int id);
        public IEnumerable<T> GetAll(bool withAsNoTracking = true);
        public IQueryable<T> GetIQueryable();
        public IEnumerable<T> GetIEnumerable();
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
