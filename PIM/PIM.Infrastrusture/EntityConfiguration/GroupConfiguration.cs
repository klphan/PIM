using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using PIM.Core;

namespace PIM.Infrastructure.EntityConfiguration
{
    class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            Property(g => g.ID)
                .IsRequired();


            HasRequired(g => g.GroupLeader)
                .WithOptional(e => e.IsGroupLeader)
                .Map(a => a.MapKey("GroupLeaderId"));

            //.HasForeignKey(g => g.GroupLeaderId); 
            Property(g => g.Version)
                .IsConcurrencyToken();
        }
    }
}
