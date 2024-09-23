using LinkDev.IKEA.DAL.Models.Departments;
using LinkDev.IKEA.DAL.Models.Employees;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositotries._Generic;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Departments;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.DAL.Persistance.Repositotries.Employees
{
    public class EmployeeRepository : GenericRepositories<Employee> , IEmployeeRepository
    {

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
         
        }
    }
    
       
}
