using LinkDev.IKEA.DAL.Models.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositotries._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositotries.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Employee? Get(int id);
        public IEnumerable<Employee> GetAll(bool withAsNoTracking = true);

        public IQueryable<Employee> GetAllAsIQueryable();
        public int Add(Employee entity);
        public int Update(Employee entity);
        public int Delete(int id);
    }
}
