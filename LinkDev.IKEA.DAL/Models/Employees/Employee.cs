
using System.ComponentModel.DataAnnotations;

using LinkDev.IKEA.DAL.Models.Common.Enums;

namespace LinkDev.IKEA.DAL.Models.Employees
{
    public class Employee : ModelBase
    {

        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateOnly HiringDate { get; set; }
        public Gender Gender  { get; set; }
        public EmpType EmployeeType { get; set; }
    }
}
