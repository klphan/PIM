using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Core;
using PIM.Infrastructure;
using PIM.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace PIM.Test
{
    public class PIMTest
    {

        static void Main(string[] args)
        {
            //set the context and reference data
            CreateNewContext();      

            //ProjectService createProjectService;
            //Project validProject, invalidProject;
            //AddNewProjects(out createProjectService, out validProject, out invalidProject);

            //createProjectService.Create(validProject, new List<string> { "aa1", "aa2", "aa3" });
            //createProjectService.Create(invalidProject, new List<string> { "aa1", "aa2", "aa3" });
        }

        private static void AddNewProjects(out ProjectService createProjectService, out Project validProject, out Project invalidProject)
        {
            createProjectService = new ProjectService();
            validProject = new Project
            {
                GroupId = Guid.Parse("9C3F7E17-8849-4088-8F55-2D9E02435DE8"),
                ProjectNumber = 1112,
                Name = "projectname1",
                Customer = "customer1",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };
            invalidProject = new Project
            {
                GroupId = Guid.Parse("1B8616BC-E30B-4FF8-BA98-CDE8A01D1E01"),
                ProjectNumber = 1112,
                Name = "projectname1",
                Customer = "customer1",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };
        }

        private static void CreateNewContext()
        {
            PIMContext context = new PIMContext();
            PIMInitializeDB db = new PIMInitializeDB();
            Database.SetInitializer(db);
            var result = context.Groups.ToList();
            context.Dispose();
        }
    }
}
