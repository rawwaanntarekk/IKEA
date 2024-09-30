using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Employees;

namespace LinkDev.IKEA.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository EmployeeRepository  => new EmployeeRepository(_context);

        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_context);
   
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
          


        
    }
}
