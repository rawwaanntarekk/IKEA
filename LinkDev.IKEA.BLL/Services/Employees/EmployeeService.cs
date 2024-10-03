using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Models.Common.Enums;
using LinkDev.IKEA.DAL.Models.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService(IUnitOfWork _UnitOfWork,
                                 IAttachmentService _attachmentService) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeGeneralDTO>> GetEmployeesAsync(string Search)
        {
            var employees = await (_UnitOfWork.EmployeeRepository
                .GetIQueryable()
                .Where(e => !e.IsDeleted && (string.IsNullOrEmpty(Search) || e.Name.ToLower().Contains(Search.ToLower())))
                .Include(e => e.Department)
                .Select(e => new EmployeeGeneralDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    IsActive = e.IsActive,
                    Email = e.Email,
                    Gender = e.Gender.ToString(),
                    EmployeeType = e.EmployeeType.ToString(),
                    Department = e.Department != null ? e.Department.Name : "No Department",
                    Image = e.ImageUrl
                    
                }).ToListAsync());
                return  employees;

        }
        public async Task<EmployeeDetailsDTO?> GetEmployeeAsync(int id)
        {
            var employee =await  _UnitOfWork.EmployeeRepository.GetAsync(id);
            if (employee is { })
                return new EmployeeDetailsDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Address = employee.Address,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    Phone = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    Department = employee.Department!,
                    Image = employee.ImageUrl
                };

            return null;

        }
        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDTO employee)
        {
            
            var CreatedEmployee = new Employee
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Salary =(decimal) employee.Salary!,
                IsActive = employee.IsActive,
                Email = employee.Email!,
                PhoneNumber = employee.Phone!,
                HiringDate = (DateOnly)employee.HiringDate!,
                Gender = (Gender) employee.Gender!,
                EmployeeType = (EmpType) employee.EmployeeType!,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = employee.DepartmentId,                
            };

            if (employee.Image is not null)
                CreatedEmployee.ImageUrl =await _attachmentService.UploadAsync(employee.Image, "images");


            _UnitOfWork.EmployeeRepository.Add(CreatedEmployee);

            return await _UnitOfWork.CompleteAsync();
        }
        public async Task<int> UpdateEmployeeAsync(int id , UpdatedEmployeeDTO employee)
        {
            var employeeToUpdate = new Employee
            {
                Id = id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email!,
                PhoneNumber = employee.Phone!,
                HiringDate = employee.HiringDate,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = employee.DepartmentId

            };

            _UnitOfWork.EmployeeRepository.Update(employeeToUpdate);

            return await _UnitOfWork.CompleteAsync();
           
        }
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
           var employeeRepo = _UnitOfWork.EmployeeRepository;
           var employee = await employeeRepo.GetAsync(id);
           if (employee is { })
                employeeRepo.Delete(employee);

            return await  _UnitOfWork.CompleteAsync() > 0;
         


        }

       

       

       
    }
}
