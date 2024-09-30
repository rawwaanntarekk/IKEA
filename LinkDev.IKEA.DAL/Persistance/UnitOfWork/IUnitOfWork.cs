using LinkDev.IKEA.DAL.Persistance.Repositotries.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Employees;


namespace LinkDev.IKEA.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepository  EmployeeRepository { get; }
        IDepartmentRepository  DepartmentRepository { get;}
        Task<int> CompleteAsync();

    }
}
