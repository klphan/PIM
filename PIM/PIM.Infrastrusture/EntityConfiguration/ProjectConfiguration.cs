using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

using PIM.Core;

namespace PIM.Infrastructure.EntityConfiguration
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            Property(p => p.ID)
                .IsRequired();

            Property(p => p.ProjectNumber)
                .IsRequired()
                .HasPrecision(4, 0);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            //Foreign key to Group
            //Indepedent Association so use the Map method

            HasRequired(p => p.Group)
                .WithOptional(g => g.Project)
                .Map(p => p.MapKey("Group_ID"));
            // HasForeignKey only works with WithMany() relationship
            //.WithMany(g => g.Projects) 
            //.HasForeignKey(g => g.GroupId)
            //.WillCascadeOnDelete(false);

            Property(p => p.Customer)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Status)
                .IsRequired();

            Property(p => p.StartDate)
                .IsRequired();
        }

    }
}
