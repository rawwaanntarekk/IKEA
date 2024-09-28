using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentGeneralDTO> GetDepartments(string Search);

        DepartmentDetailsDTO? GetDepartmentByID(int id);

        int CreateDepartment(CreatedDepartmentDTO department);

        int UpdateDepartment(UpdatedDepartmentDTO department);

        bool deleteDepartment(int id);
    }
}
