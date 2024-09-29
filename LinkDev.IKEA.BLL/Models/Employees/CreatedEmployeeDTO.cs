using LinkDev.IKEA.DAL.Models.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class CreatedEmployeeDTO
    {
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public string Name { get; set; } = null!;

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-z]{5,10}-[a-zA-z]{4,10}$", ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Salary is required")]
        public decimal? Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Hiring Date")]
        [Required(ErrorMessage = "Hiring Date is required")]
        public DateOnly? HiringDate { get; set; }

        [Required(ErrorMessage = "You must choose a gender")]
        public Gender? Gender { get; set; }

        [Display(Name = "Employee Type")]
        [Required(ErrorMessage = "You must choose a type")]
        public EmpType? EmployeeType { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
