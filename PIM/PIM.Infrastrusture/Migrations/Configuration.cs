namespace PIM.DataAccess.Migrations
{
    using PIM.Core;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PIM.Infrastructure.PIMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PIM.Infrastructure.PIMContext context)
        {
            //  This method will be called after migrating to the latest version.
            #region Add Employees 

            #endregion
            #region Add Group

            #endregion

            //var employees = new List<Employee>
            //{
            //    new Employee
            //    {
            //        ID = new Guid(),
            //        Visa = "aa1",
            //        FirstName = "FirstName1",
            //        LastName = "LastName1",
            //        BirthDay = new DateTime (2000, 01, 01)
            //    },
            //    new Employee
            //    {
            //        ID = new Guid(),
            //        Visa = "aa2",
            //        FirstName = "FirstName2",
            //        LastName = "LastName2",
            //        BirthDay = new DateTime (1999, 01, 01)
            //    },
            //    new Employee
            //    {
            //        ID = new Guid(),
            //        Visa = "aa3",
            //        FirstName = "FirstName3",
            //        LastName = "LastName3",
            //        BirthDay = new DateTime (1999, 01, 01)
            //    },
            //    new Employee
            //    {
            //        ID = new Guid(),
            //        Visa = "aa4",
            //        FirstName = "FirstName4",
            //        LastName = "LastName4",
            //        BirthDay = new DateTime (1998, 01, 01)
            //    },
            //    new Employee
            //    {
            //        ID = new Guid(),
            //        Visa = "aa5",
            //        FirstName = "FirstName5",
            //        LastName = "LastName5",
            //        BirthDay = new DateTime (1996, 01, 01)
            //    }
            //};


            

            Employee emp1 = new Employee
            {
                ID = new Guid(),
                Visa = "aa1",
                FirstName = "FirstName1",
                LastName = "LastName1",
                BirthDay = new DateTime(2000, 01, 01)
            };

            Employee emp2 = new Employee
            {
                
                    ID = new Guid(),
                    Visa = "aa2",
                    FirstName = "FirstName2",
                    LastName = "LastName2",
                    BirthDay = new DateTime (1999, 01, 01)
                

            };
            Employee emp3 = new Employee
            {
                ID = new Guid(),
                Visa = "aa3",
                FirstName = "FirstName3",
                LastName = "LastName3",
                BirthDay = new DateTime(1999, 01, 01)
            };
            Employee emp4 = new Employee
            {
                ID = new Guid(),
                Visa = "aa4",
                FirstName = "FirstName4",
                LastName = "LastName4",
                BirthDay = new DateTime(1999, 01, 01)
            };
            Employee emp5 = new Employee
            {
                ID = new Guid(),
                Visa = "aa5",
                FirstName = "FirstName5",
                LastName = "LastName5",
                BirthDay = new DateTime(1999, 01, 01)
            };
            context.Employees.Add(emp1);
            context.Employees.Add(emp2);
            context.Employees.Add(emp3);
            context.Employees.Add(emp4);
            context.Employees.Add(emp5);

            var groups = new List<Group>
            {
                new Group
                {
                    ID = new Guid(),
                    GroupLeader = emp1
                },
                new Group
                {
                    ID = new Guid(),
                    GroupLeader = emp2
                },
                new Group
                {
                    ID = new Guid(),
                    GroupLeader = emp3
                },
            };
            foreach (var group in groups)
            {
                context.Groups.Add(group);
            }

            context.SaveChanges();
            base.Seed(context);
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
