using LinkDev.IKEA.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositotries.Departments
{
    public interface IDepartmentRepository
    {
        public Department? Get(int id);
        public IEnumerable<Department> GetAll(bool withAsNoTracking = true);
        public int Add(Department entity);
        public int Update(Department entity);
        public int Delete(int id);

    }
}
