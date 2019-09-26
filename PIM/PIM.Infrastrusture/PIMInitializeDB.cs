using PIM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Core;

namespace PIM.Infrastructure
{
    public class PIMInitializeDB : DropCreateDatabaseAlways<PIMContext>
    {
        protected override void Seed(PIMContext context)
        {

            Employee emp1, emp2, emp3;

            AddEmployees(context, out emp1, out emp2, out emp3);

            Group group1, group2, group3;
            AddGroups(context, emp1, emp2, emp3, out group1, out group2, out group3);

            AddProjects(context, group1, group2, group3);

            
            context.SaveChanges();
            base.Seed(context);
        }

        private static void AddProjects(PIMContext context, Group group1, Group group2, Group group3)
        {
            Project project1 = new Project
            {
                GroupId = group1.ID,
                ProjectNumber = 1111,
                Name = "name1",
                Customer = "c1",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };
            Project project2 = new Project
            {
                GroupId = group2.ID,
                ProjectNumber = 1112,
                Name = "name2",
                Customer = "c2",
                Status = Status.Planned,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };
            Project project3 = new Project
            {
                GroupId = group3.ID,
                ProjectNumber = 1113,
                Name = "name3",
                Customer = "c3",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            };

            Project projectUpdateSelected = new Project
            {
                ID = Guid.Parse("7571520C-8DAD-4417-A233-07B9A328694B"),
                GroupId = group3.ID,
                ProjectNumber = 1117,
                Name = "toUpdate",
                Customer = "toUpdateCustomer",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            };
            context.Projects.AddRange(new List<Project> { project1, project2, project3, projectUpdateSelected });
            context.SaveChanges();
        }
        private static void AddGroups(PIMContext context, Employee emp1, Employee emp2, Employee emp3, out Group group1, out Group group2, out Group group3)
        {
            group1 = new Group
            {
                ID = Guid.Parse("793243BB-B9E2-4208-AF12-36E4491A2EEE"),
                GroupLeader = emp1
            };

            group2 = new Group
            {
                ID = Guid.Parse("95463135-09DA-420B-AC0F-63E0EDC6CA44"),
                GroupLeader = emp2
            };
            group3 = new Group
            {
                ID = Guid.Parse("BFD6A5CD-0C80-41E3-8A4A-AF90B9F0FF24"),
                GroupLeader = emp3
            };


            context.Groups.AddRange(new List<Group> { group1, group2, group3});
            context.SaveChanges();
        }
        private static void AddEmployees(PIMContext context, out Employee emp1, out Employee emp2, out Employee emp3)
        {
            emp1 = new Employee
            {
                Visa = "aa1",
                FirstName = "FirstName1",
                LastName = "LastName1",
                BirthDay = new DateTime(2000, 01, 01)
            };
            emp2 = new Employee
            {

                Visa = "aa2",
                FirstName = "FirstName2",
                LastName = "LastName2",
                BirthDay = new DateTime(1999, 01, 01)


            };
            emp3 = new Employee
            {
                Visa = "aa3",
                FirstName = "FirstName3",
                LastName = "LastName3",
                BirthDay = new DateTime(1999, 01, 01)
            };
            Employee emp4 = new Employee
            {
                Visa = "aa4",
                FirstName = "FirstName4",
                LastName = "LastName4",
                BirthDay = new DateTime(1999, 01, 01)
            };
            Employee emp5 = new Employee
            {
                Visa = "aa5",
                FirstName = "FirstName5",
                LastName = "LastName5",
                BirthDay = new DateTime(1999, 01, 01)
            };
            context.Employees.AddRange(new List<Employee> { emp1, emp1, emp3, emp4, emp5} );
          
            context.SaveChanges();
        }
    }
}

