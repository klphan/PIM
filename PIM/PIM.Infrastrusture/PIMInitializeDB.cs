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
        public static readonly Guid Group1Id = Guid.Parse("793243BB-B9E2-4208-AF12-36E4491A2EEE");
        public static readonly Guid Group2Id = Guid.Parse("95463135-09DA-420B-AC0F-63E0EDC6CA44");
        public static readonly Guid Group3Id = Guid.Parse("BFD6A5CD-0C80-41E3-8A4A-AF90B9F0FF24");
        public static readonly Guid Group4Id = Guid.Parse("a4b210ad-67cf-49a6-a87e-19f7a0083bbd");
        public static readonly Guid Group5Id = Guid.Parse("f341fe9d-cbb5-42f9-bb9a-daccd07ca3d1");

       

        protected override void Seed(PIMContext context)
        {
            Employee emp1, emp2, emp3, emp4, emp5;
            AddEmployees(context, out emp1, out emp2, out emp3, out emp4, out emp5);
            AddGroups(context, emp1, emp2, emp3, emp4, emp5);
            AddProjects(context);
            context.SaveChanges();
            base.Seed(context);
        }

        private static void AddProjects(PIMContext context)
        {
            Project project1 = new Project
            {
                GroupId = Group1Id,
                ProjectNumber = 1111,
                Name = "Moonshine",
                Customer = "AnalytIQ",
                Status = Status.New,
                StartDate = new DateTime(2019, 10, 10),

            };
            Project project2 = new Project
            {
                GroupId = Group2Id,
                ProjectNumber = 1112,
                Name = "Infinitly",
                Customer = "Vantage",
                Status = Status.Planned,
                StartDate = new DateTime(2014, 9, 13),
                EndDate = new DateTime(2017, 7, 15)
            };
            Project project3 = new Project
            {
                GroupId = Group3Id,
                ProjectNumber = 1113,
                Name = "Cyclone",
                Customer = "Optiwise",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            };
            Project project4 = new Project
            {
                GroupId = Group4Id,
                ProjectNumber = 1114,
                Name = "Motorry",
                Customer = "CreamDeLaCar",
                Status = Status.Planned,
                StartDate = new DateTime(2010, 8, 19),
                EndDate = new DateTime(2012, 7, 15)

            };
            Project project5 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 1115,
                Name = "Collabbra",
                Customer = "BluePeakLogic",
                Status = Status.Planned,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            };
            Project project6 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 1116,
                Name = "X Lab",
                Customer = "VoxiDoxi",
                Status = Status.Finished,
                StartDate = new DateTime(2018, 7, 3),
                EndDate = new DateTime(2019, 7, 15)

            };
            Project project7 = new Project
            {
                GroupId = Group4Id,
                ProjectNumber = 1117,
                Name = "GAVYL",
                Customer = "ONEWill",
                Status = Status.New,
                StartDate = new DateTime(2019, 7, 15)

            };

            Project project8 = new Project
            {
                GroupId = Group4Id,
                ProjectNumber = 1118,
                Name = "SmartPave",
                Customer = "Oak & Stone",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 7, 12),
                EndDate = new DateTime(2019, 6, 15)

            };
            Project project9 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 1119,
                Name = "Qube",
                Customer = "Paragon Construction",
                Status = Status.InProgress,
                StartDate = new DateTime(2018, 11, 30),


            };
            Project testProject = new Project
            {
                ID = Guid.Parse("7571520C-8DAD-4417-A233-07B9A328694B"),
                GroupId = Group3Id,
                ProjectNumber = 1127,
                Name = "Cybersify",
                Customer = "MavernPoint",
                Status = Status.InProgress,
                StartDate = new DateTime(2016, 7, 15),

            };
            Project project10 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11110,
                Name = "Widget-Fix",
                Customer = "Paragon Construction",
                Status = Status.InProgress,
                StartDate = new DateTime(2017, 12, 30),


            };
            Project project11 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11111,
                Name = "Aurora",
                Customer = "Roboville",
                Status = Status.InProgress,
                StartDate = new DateTime(2010, 1, 2),


            };
            Project project12 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11112,
                Name = "Quadro",
                Customer = "quickina",
                Status = Status.InProgress,
                StartDate = new DateTime(2008, 7, 26),


            };
            Project project13 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 1119,
                Name = "Poseidon",
                Customer = "Arizy",
                Status = Status.InProgress,
                StartDate = new DateTime(2000, 12, 31),


            };
        
            Project project14 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11114,
                Name = "Skyhawks",
                Customer = "OpticRoute",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 3, 20),


            };
            Project project15 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11115,
                Name = "Sputnik",
                Customer = "Aerotra",
                Status = Status.Finished,
                StartDate = new DateTime(2006, 1, 13),


            };
            Project project16 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11116,
                Name = "Xena",
                Customer = "Ascently",
                Status = Status.Finished,
                StartDate = new DateTime(2005, 12, 30),


            };
            Project project17 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11117,
                Name = "Anacondas",
                Customer = "Volantos",
                Status = Status.Finished,
                StartDate = new DateTime(2001, 5, 10),


            };
            Project project18 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11118,
                Name = "Captivators",
                Customer = "Uppyo",
                Status = Status.Finished,
                StartDate = new DateTime(2009, 2, 04),


            };
            Project project19 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11119,
                Name = "Falcons",
                Customer = "Munchups",
                Status = Status.Finished,
                StartDate = new DateTime(2017, 10, 30),


            };
            Project project20 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11120,
                Name = "Gladiators",
                Customer = "Blue Canyon",
                Status = Status.Finished,
                StartDate = new DateTime(2017, 12, 30),


            };
            Project project21 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11121,
                Name = "Fleetworth",
                Customer = "Birch and Forest",
                Status = Status.New,
                StartDate = new DateTime(2013, 10, 20),


            };
            Project project22 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11122,
                Name = "Innovura",
                Customer = "Paragon Construction",
                Status = Status.New,
                StartDate = new DateTime(2008, 11, 30),


            };
            Project project23 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11123,
                Name = "Jovelet",
                Customer = "Paragon Construction",
                Status = Status.New,
                StartDate = new DateTime(2006, 11, 30),


            };
            Project project24 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11124,
                Name = "GrowthGround",
                Customer = "AscendLink",
                Status = Status.New,
                StartDate = new DateTime(2003, 11, 30),


            };
            Project project25 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11125,
                Name = "TrueGenix",
                Customer = "Ascenteum",
                Status = Status.New,
                StartDate = new DateTime(2002, 11, 30),


            };
            Project project26 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11126,
                Name = "Oaksted",
                Customer = "AscendLink",
                Status = Status.New,
                StartDate = new DateTime(2015, 11, 30),


            };
            Project project27 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11127,
                Name = "Qube",
                Customer = "Ascenteum",
                Status = Status.Planned,
                StartDate = new DateTime(2014, 11, 30),


            };
            Project project28 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11128,
                Name = "VRGuru",
                Customer = "BioClix",
                Status = Status.Planned,
                StartDate = new DateTime(2011, 11, 30),


            };
            Project project29 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11129,
                Name = "SkilledRite",
                Customer = "Xpressible",
                Status = Status.Planned,
                StartDate = new DateTime(2019, 11, 30),


            };
            Project project30 = new Project
            {
                GroupId = Group5Id,
                ProjectNumber = 11130,
                Name = "Puralogy",
                Customer = "Nestvia",
                Status = Status.Planned,
                StartDate = new DateTime(2018, 11, 30),


            };
            context.Projects.AddRange(new List<Project> { project1, project2, project3, project4, project5, project6,
                project7, project8, project9, testProject, project10, project11, project12, project13, project14, project15,
                project16, project17, project18, project19, project20, project21, project22, project23, project24, project25,
                project26, project27, project28, project29, project30 });
            context.SaveChanges();
        }
        private static void AddGroups(PIMContext context, Employee emp1, Employee emp2, Employee emp3, Employee emp4,
            Employee emp5)
        {
            Group group1 = new Group
            {
                ID = Guid.Parse("793243BB-B9E2-4208-AF12-36E4491A2EEE"),
                GroupLeader = emp1
            };

            Group group2 = new Group
            {
                ID = Guid.Parse("95463135-09DA-420B-AC0F-63E0EDC6CA44"),
                GroupLeader = emp2
            };
            Group group3 = new Group
            {
                ID = Guid.Parse("BFD6A5CD-0C80-41E3-8A4A-AF90B9F0FF24"),
                GroupLeader = emp3
            };
            Group group4 = new Group
            {
                ID = Guid.Parse("a4b210ad-67cf-49a6-a87e-19f7a0083bbd"),
                GroupLeader = emp4
            };
            Group group5 = new Group
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

