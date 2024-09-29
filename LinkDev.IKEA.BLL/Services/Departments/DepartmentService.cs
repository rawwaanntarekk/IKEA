using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.DAL.Models.Departments;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        public async Task<IEnumerable<DepartmentGeneralDTO>> GetDepartmentsAsync(string search)
        {

            return await (_unitOfWork.DepartmentRepository.GetIQueryable()
                .Where(d => !d.IsDeleted && (string.IsNullOrEmpty(search)|| d.Name.ToLower().Contains(search.ToLower())))
                .Select(d => new DepartmentGeneralDTO
                {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                Description = d.Description,
                CreationDate = d.CreationDate
            }).AsNoTracking().ToListAsync());
           
        }
        public async Task<DepartmentDetailsDTO?> GetDepartmentByIDAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
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
        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDTO department)
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
            return await _unitOfWork.CompleteAsync();

        }
        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO department)
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
            return await  _unitOfWork.CompleteAsync();

        }
        public async Task<bool> deleteDepartmentAsync(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            var department = await  departmentRepo.GetAsync(id);
            if (department is { })
                departmentRepo.Delete(department);

            return await _unitOfWork.CompleteAsync() > 0;
        }

       
    }
}
