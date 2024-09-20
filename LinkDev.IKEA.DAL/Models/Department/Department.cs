using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Models.Department
{
    // This is the data the will be in the database table.
    internal class Department : ModelBase
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // The date the deprtment created in the company.
        public DateOnly CreationDate { get; set; }

    }
}
