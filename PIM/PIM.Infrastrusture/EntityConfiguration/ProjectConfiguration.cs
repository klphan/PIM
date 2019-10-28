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


            Property(p => p.ProjectNumber)
                .IsRequired();

            //Indepedent Association so use the Map method

            HasRequired(p => p.Group)

            // HasForeignKey only works with WithMany() relationship
            .WithMany(g => g.Projects)
            .HasForeignKey(g => g.GroupId);

            Property(p => p.Version)
                .IsConcurrencyToken();
       
        }

    }
}
