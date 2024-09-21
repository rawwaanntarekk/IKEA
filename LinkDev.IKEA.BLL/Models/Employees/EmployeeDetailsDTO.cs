using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class EmployeeDetailsDTO
    {
        public int Id { get; set; }
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
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public string Gender { get; set; } = null!;

        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; } = null!;
    }
}
