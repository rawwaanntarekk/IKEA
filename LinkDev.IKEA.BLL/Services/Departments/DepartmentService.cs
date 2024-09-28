using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.DAL.Models.Departments;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        public IEnumerable<DepartmentGeneralDTO> GetDepartments(string search)
        {
            return _unitOfWork.DepartmentRepository.GetAllAsIQueryable()
                .Where(d => !d.IsDeleted && (string.IsNullOrEmpty(search)|| d.Name.ToLower().Contains(search.ToLower())))
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
            var department = _unitOfWork.DepartmentRepository.Get(id);
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

            _unitOfWork.DepartmentRepository.Add(newDept);
            return _unitOfWork.Complete();

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

            _unitOfWork.DepartmentRepository.Update(dept);
            return _unitOfWork.Complete();

        }
        public bool deleteDepartment(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            var department = departmentRepo.Get(id);
            if (department is { })
                departmentRepo.Delete(id);

            return _unitOfWork.Complete() > 0;
        }


    }
}
