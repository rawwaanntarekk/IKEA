using LinkDev.IKEA.DAL.Models.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
           builder.Property(D => D.Id).UseIdentityColumn();
           builder.Property(D => D.Name).HasColumnType("nvarchar(50)");
           builder.Property(D => D.Name).HasColumnType("nvarchar(50)");
           builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
           builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");

        }
    }
}
