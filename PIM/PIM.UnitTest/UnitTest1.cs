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
                GroupId = Guid.Parse("793243BB-B9E2-4208-AF12-36E4491A2EEE"),
                ProjectNumber = 1114,
                Name = "projectname4",
                Customer = "customer4",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2017, 7, 15)
            };

            Project invalidProject = new Project
            {
                GroupId = Guid.Parse("95463135-09DA-420B-AC0F-63E0EDC6CA44"),
                ProjectNumber = 1114,
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
