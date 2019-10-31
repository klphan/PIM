using System.Data.Entity.ModelConfiguration;
using PIM.Core.Entities;

namespace PIM.Infrastructure.EntityConfiguration
{
    class ProjectEmployeeConfiguration : EntityTypeConfiguration<ProjectEmployee>
    {
        public ProjectEmployeeConfiguration()
        {
            HasOptional(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            HasOptional(pe => pe.Employee)
               .WithMany(e => e.ProjectEmployees)
               .HasForeignKey(pe => pe.EmployeeId);

        }
    }
}
