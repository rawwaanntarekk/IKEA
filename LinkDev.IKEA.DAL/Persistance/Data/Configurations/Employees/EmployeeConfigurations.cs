using LinkDev.IKEA.DAL.Models.Common.Enums;
using LinkDev.IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Employees
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
          builder.Property(E => E.Name).HasColumnType("nvarchar(50)");
          builder.Property(E => E.Address).HasColumnType("nvarchar(100)");
          builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");
          builder.Property(E => E.CreatedOn).HasDefaultValue(DateTime.UtcNow);
          builder.Property(E => E.Gender)
                 .HasConversion((gender) => gender.ToString(),
                 (gender) => (Gender) Enum.Parse(typeof(Gender), gender));

          builder.Property(E => E.EmployeeType)
                 .HasConversion(
                 (empType) => empType.ToString(),
                 (empType) => (EmpType)Enum.Parse(typeof(EmpType), empType));





        }
    }
}
