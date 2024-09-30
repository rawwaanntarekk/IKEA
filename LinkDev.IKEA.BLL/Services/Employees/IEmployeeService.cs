using LinkDev.IKEA.BLL.Models.Employees;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeGeneralDTO>> GetEmployeesAsync(string Search);
        Task<EmployeeDetailsDTO?> GetEmployeeAsync(int id);
        Task<int> CreateEmployeeAsync(CreatedEmployeeDTO  employee);
        Task<int> UpdateEmployeeAsync(int id ,UpdatedEmployeeDTO employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
