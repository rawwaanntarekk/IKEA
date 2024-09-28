using LinkDev.IKEA.DAL.Persistance.Repositotries.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Employees;


namespace LinkDev.IKEA.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository  EmployeeRepository { get; }
        public IDepartmentRepository  DepartmentRepository { get;}
        int Complete();

    }
}
