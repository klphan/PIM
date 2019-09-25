using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using PIM.Core;

namespace PIM.Infrastructure.EntityConfiguration
{
    class ProjectEmployeeConfiguration : EntityTypeConfiguration<ProjectEmployee>

    {
        public ProjectEmployeeConfiguration()
        {

            HasRequired(pe => pe.Project)
                 .WithOptional(p => p.ProjectEmployee)
                 .Map(pe => pe.MapKey("Project_ID"));

            HasRequired(pe => pe.Employee)
                .WithOptional(e => e.ProjectEmployee)
                .Map(pe => pe.MapKey("Employee_ID"));
        }
    }
}
