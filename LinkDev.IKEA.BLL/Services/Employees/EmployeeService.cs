using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Models.Common.Enums;
using LinkDev.IKEA.DAL.Models.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositotries.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        public int CreateEmployee(CreatedEmployeeDTO employee)
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
                LastModifiedOn = DateTime.UtcNow
            };

            return employeeRepository.Add(CreatedEmployee);
        }

        public bool DeleteEmployee(int id)
        {
           var employee = employeeRepository.Get(id);
           if (employee is { })
                return employeeRepository.Delete(employee) > 0;
           return false;


        }

        public IEnumerable<EmployeeGeneralDTO> GetAllEmployees()
        {
            var employee = employeeRepository
                .GetIEnumerable()
                .Where(e => !e.IsDeleted)
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
                }).ToList();
            return employee;

        }

        public EmployeeDetailsDTO? GetEmployee(int id)
        {
            var employee = employeeRepository.Get(id);
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
                    Gender = nameof(employee.Gender),
                    EmployeeType = nameof(employee.EmployeeType),
                };

            return null;

        }

        public int UpdateEmployee(int id, UpdatedEmployeeDTO employee)
        {
            var CreatedEmployee = new Employee
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
                LastModifiedOn = DateTime.UtcNow
            };
            return employeeRepository.Update(CreatedEmployee);
        }
    }
}
