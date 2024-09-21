using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.DAL.Models.Department;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Departments;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        public IEnumerable<DepartmentGeneralDTO> GetAllDepartments()
        {
            return _departmentRepository.GetAllAsIQueryable()
                .Select(d => new DepartmentGeneralDTO
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                Description = d.Description,
                CreationDate = d.CreationDate
            }).AsNoTracking().ToList();
           
        }

        public DepartmentDetailsDTO? GetDepartmentByID(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is { })
                return new DepartmentDetailsDTO
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,

                };
            return null;
        }
        public int CreateDepartment(CreatedDepartmentDTO department)
        {
            var newDept = new Department
            {

                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow

            };

            return _departmentRepository.Add(newDept);

        }
        public int UpdateDepartment(UpdatedDepartmentDTO department)
        {
          var dept = new Department
            {
              Id = department.Id,
              Code = department.Code,
              Name = department.Name,
              Description = department.Description,
              CreationDate = department.CreationDate,
              LastModifiedBy = 1,
              LastModifiedOn = DateTime.UtcNow

          };

            return _departmentRepository.Update(dept);
           

        }

        public bool deleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is { })
                return _departmentRepository.Delete(id) > 0;
            return false;
        }


    }
}
