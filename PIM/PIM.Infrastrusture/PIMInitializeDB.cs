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
            AddProjects(context);

            Employee emp1, emp2, emp3;
            AddEmployees(context, out emp1, out emp2, out emp3);
            AddGroups(context, emp1, emp2, emp3);
            base.Seed(context);
        }

        private static void AddGroups(PIMContext context, Employee emp1, Employee emp2, Employee emp3)
        {
            var groups = new List<Group>
                {
                    new Group
                    {
                        GroupLeader = emp1
                    },
                    new Group
                    {
                        GroupLeader = emp2
                    },
                    new Group
                    {
                        GroupLeader = emp3
                    },
                };
            foreach (var group in groups)
            {
                context.Groups.Add(group);
            }


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
            context.Employees.Add(emp1);
            context.Employees.Add(emp2);
            context.Employees.Add(emp3);
            context.Employees.Add(emp4);
            context.Employees.Add(emp5);
        }

        private static void AddProjects(PIMContext context)
        {
            context.Projects.Add(
            new Project
            {
                Group_ID = Guid.NewGuid(),
                ProjectNumber = 1111,
                Name = "name1",
                Customer = "c1",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            });
            context.Projects.Add(
            new Project
            {
                Group_ID = Guid.NewGuid(),
                ProjectNumber = 1112,
                Name = "name2",
                Customer = "c2",
                Status = Status.Planned,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            });
            context.Projects.Add(
            new Project
            {
                Group_ID = Guid.NewGuid(),
                ProjectNumber = 1113,
                Name = "name3",
                Customer = "c3",
                Status = Status.Finished,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)

            }
        );
            context.SaveChanges();
        }
    }
}

