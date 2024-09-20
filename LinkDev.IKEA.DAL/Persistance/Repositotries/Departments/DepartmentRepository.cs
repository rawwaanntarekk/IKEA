using LinkDev.IKEA.DAL.Models.Department;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.DAL.Persistance.Repositotries.Departments
{
    // Primary Constructor
    // Asking CLR for object of ApplicationDbContext Implicitly
    // Will be avaailble upon the object
    internal class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {
        public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return dbContext.Departments.AsNoTracking().ToList();

            return dbContext.Departments.ToList();
        }

        public Department? Get(int id)
        {
           var department = dbContext.Departments.Find(id);
           return department;
        }

        public IQueryable<Department> GetAllAsIQueryable()
        {
            return dbContext.Departments;
        }

        public int Add(Department entity)
        {
           dbContext.Departments.Add(entity);
            return dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
           dbContext.Departments.Update(entity);
          return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var department = dbContext.Departments.Find(id);
            if (department is { })
                dbContext.Departments.Remove(department);

            return dbContext.SaveChanges();
        }



    }
   
}
