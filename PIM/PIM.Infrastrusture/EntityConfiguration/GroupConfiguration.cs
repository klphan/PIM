using System.Data.Entity.ModelConfiguration;
using PIM.Core.Entities;

namespace PIM.Infrastructure.EntityConfiguration
{
    class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            Property(g => g.Id)
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
