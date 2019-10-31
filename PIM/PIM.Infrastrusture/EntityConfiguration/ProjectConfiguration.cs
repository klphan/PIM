using System.Data.Entity.ModelConfiguration;
using PIM.Core.Entities;

namespace PIM.Infrastructure.EntityConfiguration
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {

            Property(p => p.ProjectNumber)
                .IsRequired();
            //Independent Association so use the Map method

            HasRequired(p => p.Group)

            // HasForeignKey only works with WithMany() relationship
            .WithMany(g => g.Projects)
            .HasForeignKey(g => g.GroupId);

            Property(p => p.Version)
                .IsConcurrencyToken();
       
        }

    }
}
