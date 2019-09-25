using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Core;
using PIM.Infrastructure;
using PIM.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PIM.Test
{
    public class PIMTest
    {

        static void Main(string[] args)
        {
            //create a new database with seed data
            //using (var ctx = new PIMContext())
            //{

            //    PIMInitializeDB db = new PIMInitializeDB();
            //    System.Data.Entity.Database.SetInitializer(db);

            //    ctx.SaveChanges();
            //}

            //using (var unitOfWork = new UnitOfWork(new PIMContext()))
            //{
            //    // Example1


           
            //    unitOfWork.Commit();

            //}
            ProjectService createProjectService = new ProjectService();

            Project validProject = new Project
            {
                Group_ID = Guid.Parse("3676046c-383c-4b9c-b53a-cc148c3c3e82"),
                ProjectNumber = 1112,
                Name = "projectname1",
                Customer = "customer1",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };

            Project invalidProject = new Project
            {
                Group_ID = Guid.Parse("1B8616BC-E30B-4FF8-BA98-CDE8A01D1E01"),
                ProjectNumber = 1112,
                Name = "projectname1",
                Customer = "customer1",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };

            createProjectService.Create(validProject, new List<string> { "aa1", "aa2", "aa3" });
            createProjectService.Create(invalidProject, new List<string> { "aa1", "aa2", "aa3" });
        }
    }
}
