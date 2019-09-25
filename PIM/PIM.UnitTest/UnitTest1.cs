using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIM.Core;
using PIM.Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace PIM.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
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
            var result = createProjectService.Search(new ProjectCriteria { Text = "customer1" });
            //(createProjectService.Create(validProject, new List<string>()));
            Assert.IsNotNull(result);
        }

    }
}
