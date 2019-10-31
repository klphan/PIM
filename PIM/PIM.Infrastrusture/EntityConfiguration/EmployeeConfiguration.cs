using System.Data.Entity.ModelConfiguration;
using PIM.Core.Entities;

namespace PIM.Infrastructure.EntityConfiguration
{
    class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            Property(e => e.Id)
                .IsRequired();

            Property(e => e.Visa)
                .IsRequired()
                .HasMaxLength(3);

            Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.BirthDay)
                .IsRequired();

            Property(e => e.Version)
                .IsConcurrencyToken();
        }
    }
}
