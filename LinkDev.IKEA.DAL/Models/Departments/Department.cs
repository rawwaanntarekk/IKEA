using LinkDev.IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Models.Departments
{
    // This is the data the will be in the database table.
    public class Department : ModelBase
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // The date the deprtment created in the company.
        public DateOnly CreationDate { get; set; }
        
        // Navigational Property Many
        public virtual ICollection<Employee> Employees {get; set;} = new HashSet<Employee>();

    }
}
