using LinkDev.IKEA.DAL.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class EmployeeGeneralDTO
    {
       
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;

        public string Department { get; set; } = null!;
    }
}
