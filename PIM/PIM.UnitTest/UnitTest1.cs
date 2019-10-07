using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIM.Core;
using PIM.Core.Exceptions;
using PIM.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PIM.UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        ProjectService service = new ProjectService();
        [TestMethod]
        public void TestAddValidProject()
        {
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
            service.Create(validProject, new List<string> { "aa1", "aa2", "aa3" });
            var result = service.Search(new ProjectCriteria{Text = "projectname4"});
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void TestInvalidProjectNumber()
        {

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

            service.Create(invalidProject, new List<string> { "aa1", "aa2", "aa3" });
            //(createProjectService.Create(validProject, new List<string>()));
            Assert.ThrowsException<InvalidProjectNumberException>(() =>
            service.Create(invalidProject, new List<string> { "aa1", "aa2", "aa3" }));
        }

        [TestMethod]
        public void TestInvalidEndDate()
        {
            Project invalidEndDate = new Project
            {
                GroupId = Guid.Parse("793243BB-B9E2-4208-AF12-36E4491A2EEE"),
                ProjectNumber = 1119,
                Name = "projectname4",
                Customer = "customer4",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2016, 5, 15)
            };

            Assert.ThrowsException<InvalidEndDateException>(() =>
           service.Create(invalidEndDate, new List<string> { "aa1", "aa2", "aa3" }));
        }

        [TestMethod]
        public void SearchProjects()
        {
            // search by text && status match
            ProjectCriteria textStatus = new ProjectCriteria { Text = "c3", Status = Status.Finished };
            IEnumerable<Project> textStatusList = service.Search(textStatus);
            Assert.IsTrue(textStatusList.Any());
            // search by text && status => expect empty list mismatch text status
            ProjectCriteria invalidTextStatus = new ProjectCriteria { Text = "c3", Status = Status.New };
            IEnumerable<Project> invalidTextStatusList = service.Search(invalidTextStatus);
            Assert.IsTrue(!invalidTextStatusList.Any());
            //search by text
            ProjectCriteria textOnly = new ProjectCriteria { Text = "c3" };
            IEnumerable<Project> textOnlyList = service.Search(textOnly);
            Assert.IsTrue(textOnlyList.Any());
            //search by status
            ProjectCriteria status = new ProjectCriteria { Status = Status.New };
            IEnumerable<Project> statusList = service.Search(status);
            Assert.IsTrue(statusList.Any());
            //empty criteria
            ProjectCriteria noCriteria = new ProjectCriteria();
            IEnumerable<Project> noCriteriaList = service.Search(noCriteria);
            Assert.IsTrue(noCriteriaList.Any());

        }

        [TestMethod]
        public void TestUpdateProject()
        {

            Project toUpdateProject = new Project
            {
                GroupId = Guid.Parse("793243BB-B9E2-4208-AF12-36E4491A2EEE"),
                ID = Guid.Parse("7571520C-8DAD-4417-A233-07B9A328694B"),
                ProjectNumber = 1117,
                Name = "updated",
                Customer = "updatedCustomer",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2016, 10, 15)
            };
            service.Update(toUpdateProject, new List<string> { "aa1", "aa2", "aa3"});
            var foundUpdated = service.Search(new ProjectCriteria { Text = "updated" });
            Assert.IsTrue(foundUpdated.Any());
        }

        [TestMethod]
        public void TestDeleteProject()
        {
            Project toDeleteProject = new Project
            {
                //Delete project with matching ID but different info
                GroupId = Guid.Parse("793243BB-B9E2-4208-AF12-36E4491A2EEE"),
                ID = Guid.Parse("7571520C-8DAD-4417-A233-07B9A328694B"),
                ProjectNumber = 1117,
                Name = "updated",
                Customer = "updatedCustomer",
                Status = Status.New,
                StartDate = new DateTime(2016, 7, 15),
                EndDate = new DateTime(2016, 10, 15)
            };

            service.Delete(toDeleteProject.ID);
            var foundDeleted = service.Search(new ProjectCriteria { Text = "updated" });
            Assert.IsTrue(!foundDeleted.Any());
        }

    }
}


