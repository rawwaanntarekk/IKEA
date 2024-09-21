using LinkDev.IKEA.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeGeneralDTO> GetAllEmployees();
        EmployeeDetailsDTO? GetEmployee(int id);
        int CreateEmployee(CreatedEmployeeDTO  employee);
        int UpdateEmployee(int id, UpdatedEmployeeDTO employee);
        bool DeleteEmployee(int id);
    }
}
