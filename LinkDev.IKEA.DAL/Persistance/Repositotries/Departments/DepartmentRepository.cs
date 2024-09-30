using LinkDev.IKEA.DAL.Models.Departments;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositotries._Generic;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.DAL.Persistance.Repositotries.Departments
{
    // Primary Constructor
    // Asking CLR for object of ApplicationDbContext Implicitly
    // Will be avaailble upon the object
    public class DepartmentRepository : GenericRepositories<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

      


    }
   
}
