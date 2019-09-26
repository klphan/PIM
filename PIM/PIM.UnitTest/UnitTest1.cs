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
                GroupId = Guid.Parse("9C3F7E17-8849-4088-8F55-2D9E02435DE8"),
                ProjectNumber = 1112,
                Name = "projectname1",
                Customer = "customer1",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };

            Project invalidProject = new Project
            {
                GroupId = Guid.Parse("1B8616BC-E30B-4FF8-BA98-CDE8A01D1E01"),
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
