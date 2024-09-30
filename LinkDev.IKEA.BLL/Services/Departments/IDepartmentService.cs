using LinkDev.IKEA.BLL.Models;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentGeneralDTO>> GetDepartmentsAsync(string Search);
        Task<DepartmentDetailsDTO?> GetDepartmentByIDAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDTO department);
        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO department);
        Task<bool> deleteDepartmentAsync(int id);
    }
}
