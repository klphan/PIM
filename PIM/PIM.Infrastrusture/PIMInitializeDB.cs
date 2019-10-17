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

            Employee emp1, emp2, emp3, emp4, emp5;

            AddEmployees(context, out emp1, out emp2, out emp3, out emp4, out emp5);

            Group group1, group2, group3, group4, group5;


            AddGroups(context, emp1, emp2, emp3, emp4, emp5, out group1, out group2, out group3, out group4, out group5);

            AddProjects(context, group1, group2, group3, group4, group5);


            context.SaveChanges();
            base.Seed(context);
        }

        private static void AddProjects(PIMContext context, Group group1, Group group2, Group group3, Group group4, Group group5)
        {
            Project project1 = new Project
            {
                GroupId = group1.ID,
                ProjectNumber = 1111,
                Name = "Moonshine",
                Customer = "AnalytIQ",
                Status = Status.New,
                StartDate = new DateTime(2019, 10, 10),

            };
            Project project2 = new Project
            {
                GroupId = group2.ID,
                ProjectNumber = 1112,
                Name = "Infinitly",
                Customer = "Vantage",
                Status = Status.Planned,
                StartDate = new DateTime(2014, 9, 13),
                EndDate = new DateTime(2017, 7, 15)
            };
            Project project3 = new Project
            {
                GroupId = group3.ID,
                ProjectNumber = 1113,
                Name = "Cyclone",
                Customer = "Optiwise",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            };
            Project project4 = new Project
            {
                GroupId = group3.ID,
                ProjectNumber = 1114,
                Name = "Motorry",
                Customer = "CreamDeLaCar",
                Status = Status.Planned,
                StartDate = new DateTime(2010, 8, 19),
                EndDate = new DateTime(2012, 7, 15)

            };
            Project project5 = new Project
            {
                GroupId = group5.ID,
                ProjectNumber = 1115,
                Name = "Collabbra",
                Customer = "BluePeakLogic",
                Status = Status.Planned,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            };
            Project project6 = new Project
            {
                GroupId = group4.ID,
                ProjectNumber = 1116,
                Name = "X Lab",
                Customer = "VoxiDoxi",
                Status = Status.Finished,
                StartDate = new DateTime(2018, 7, 3),
                EndDate = new DateTime(2019, 7, 15)

            };
            Project project7 = new Project
            {
                GroupId = group4.ID,
                ProjectNumber = 1117,
                Name = "GAVYL",
                Customer = "ONEWill",
                Status = Status.New,
                StartDate = new DateTime(2019, 7, 15)

            };

            Project project8 = new Project
            {
                GroupId = group1.ID,
                ProjectNumber = 1118,
                Name = "SmartPave",
                Customer = "Oak & Stone",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 7, 12),
                EndDate = new DateTime(2019, 6, 15)

            };
            Project project9 = new Project
            {
                GroupId = group4.ID,
                ProjectNumber = 1119,
                Name = "Qube",
                Customer = "Paragon Construction",
                Status = Status.InProgress,
                StartDate = new DateTime(2018, 11, 30),


            };
            Project testProject = new Project
            {
                ID = Guid.Parse("7571520C-8DAD-4417-A233-07B9A328694B"),
                GroupId = group3.ID,
                ProjectNumber = 1117,
                Name = "Cybersify",
                Customer = "MavernPoint",
                Status = Status.InProgress,
                StartDate = new DateTime(2016, 7, 15),

            };
            context.Projects.AddRange(new List<Project> { project1, project2, project3, project4, project5, project6,
                project7, project8, project9, testProject });
            context.SaveChanges();
        }
        private static void AddGroups(PIMContext context, Employee emp1, Employee emp2, Employee emp3, Employee emp4,
            Employee emp5, out Group group1, out Group group2, out Group group3, out Group group4, out Group group5)
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
            group4 = new Group
            {
                ID = Guid.Parse("a4b210ad-67cf-49a6-a87e-19f7a0083bbd"),
                GroupLeader = emp4
            };
            group5 = new Group
            {
                ID = Guid.Parse("f341fe9d-cbb5-42f9-bb9a-daccd07ca3d1"),
                GroupLeader = emp5
            };

            context.Groups.AddRange(new List<Group> { group1, group2, group3, group4, group5 });
            context.SaveChanges();
        }
        private static void AddEmployees(PIMContext context, out Employee emp1, out Employee emp2, out Employee emp3,
            out Employee emp4, out Employee emp5)
        {
            emp1 = new Employee
            {
                Visa = "ATN",
                FirstName = "ANH THU",
                LastName = "NGUYEN",
                BirthDay = new DateTime(2000, 01, 01)
            };
            emp2 = new Employee
            {

                Visa = "MKN",
                FirstName = "VU MINH QUANG",
                LastName = "NGUYEN",
                BirthDay = new DateTime(1999, 01, 01)


            };
            emp3 = new Employee
            {
                Visa = "JPO",
                FirstName = "JAMES P",
                LastName = "ONNA",
                BirthDay = new DateTime(1999, 01, 01)
            };
            emp4 = new Employee
            {
                Visa = "PHD",
                FirstName = "HONG PHONG",
                LastName = "DANG",
                BirthDay = new DateTime(1999, 01, 01)
            };
            emp5 = new Employee
            {
                Visa = "PIH",
                FirstName = "PETER",
                LastName = "HO",
                BirthDay = new DateTime(1999, 01, 01)
            };
            context.Employees.AddRange(new List<Employee> { emp1, emp1, emp3, emp4, emp5 });

            context.SaveChanges();
        }

    }
}

