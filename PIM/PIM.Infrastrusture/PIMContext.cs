using System.Data.Entity;
using PIM.Core.Entities;
using PIM.Infrastructure.EntityConfiguration;

namespace PIM.Infrastructure
{
    public class PimContext : DbContext
    {
        public PimContext()
            : base("name=PIMContextConnectionString")
        {
           Database.SetInitializer(new PimInitializeDb());
           
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new ProjectEmployeeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
