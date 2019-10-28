using PagedList;
using PIM.Core;
using PIM.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Infrastructure.Services
{
    public class ProjectService
    {
        public void Create(Project a, List<string> members)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                Validate(a, unitOfWork, editMode: false);
                IList<Employee> validEmployees = ValidateMembers(members, unitOfWork);

                unitOfWork.Project.Add(a);
                foreach (Employee validEmployee in validEmployees)
                {
                    unitOfWork.ProjectEmployee.Add(
                        new ProjectEmployee
                        {
                            Project = a,
                            Employee = validEmployee
                        });
                }
                unitOfWork.Commit();
            }
        }
        public void Update(Project a, List<string> members)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                Validate(a, unitOfWork, editMode: true);
                IList<Employee> validEmployees = ValidateMembers(members, unitOfWork);
                var selected = unitOfWork.Project.Get();
                var selectedProject = selected.FirstOrDefault(p => p.ID == a.ID);
                if (selectedProject == null)
                {
                    throw new ProjectHasBeenDeletedException("The project you are trying to edit has been deleted by another user." +
                        "Please click cancel to return to project list");
                }
                // assign a rowVersion to Project a then carry out the update
                // after updating if the rowversion match then allow updating
                if (!(a.Version.SequenceEqual(selectedProject.Version)))
                {
                    throw new DbUpdateConcurrencyException("The project you are trying to update has been modified by another user. Please return to the Project List and try again");
                }

                selectedProject.Customer = a.Customer;
                selectedProject.GroupId = a.GroupId;
                selectedProject.Name = a.Name;
                selectedProject.StartDate = a.StartDate;
                selectedProject.EndDate = a.EndDate;
                selectedProject.Status = a.Status;
                //Get the Collection of the ProjectEmployee associated with this project
                var exProjectEmployees = unitOfWork.ProjectEmployee.Get()
                    .Where(pe => pe.ProjectId == selectedProject.ID)
                    .ToList();

                //delete all the exRecords
                foreach (ProjectEmployee pe in exProjectEmployees)
                {
                    unitOfWork.ProjectEmployee.Remove(pe.ID);
                }

                // add new all records
                foreach (Employee validEmployee in validEmployees)
                {
                    var newProjectEmployeeRecord = new ProjectEmployee
                    {
                        ProjectId = selectedProject.ID,
                        EmployeeId = validEmployee.ID
                    };
                    selectedProject.ProjectEmployees.Add(newProjectEmployeeRecord);
                }

                unitOfWork.Commit();

            }
        }
        public Project GetProjectWithEmployees(Guid id)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                var project = unitOfWork.Project.Get()
                    .Include(p => p.ProjectEmployees.Select(x => x.Employee))
                    .Where(p => p.ID == id)
                    .FirstOrDefault();
                return project;
            }
        }

        public IPagedList<Project> Search(ProjectCriteria a)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                var query = unitOfWork.Project.Get();
                if (!string.IsNullOrEmpty(a.Text))
                {
                    query = query.Where(p => p.Name.Contains(a.Text)
                                    || p.Customer.Contains(a.Text)
                                    || p.ProjectNumber.ToString().Contains(a.Text));
                }
                if (a.Status.HasValue)
                {
                    query = query.Where(p => p.Status == a.Status);
                }
                if (!string.IsNullOrEmpty(a.SortProperty))
                {
                    switch (a.SortDirection)
                    {
                        case SortDirection.Ascending:
                            switch (a.SortProperty)
                            {
                                case "ProjectNumber":
                                    query = query.OrderBy(p => p.ProjectNumber);
                                    break;
                                case "Name":
                                    query = query.OrderBy(p => p.Name);
                                    break;
                                case "Customer":
                                    query = query.OrderBy(p => p.Customer);
                                    break;
                                case "StartDate":
                                    query = query.OrderBy(p => p.StartDate);
                                    break;
                                case "Status":
                                    query = query.OrderBy(p => p.Status);
                                    break;
                            }
                            break;

                        case SortDirection.Descending:
                            switch (a.SortProperty)
                            {
                                case "ProjectNumber":
                                    query = query.OrderByDescending(p => p.ProjectNumber);
                                    break;
                                case "Name":
                                    query = query.OrderByDescending(p => p.Name);
                                    break;
                                case "Customer":
                                    query = query.OrderByDescending(p => p.Customer);
                                    break;
                                case "StartDate":
                                    query = query.OrderByDescending(p => p.StartDate);
                                    break;
                                case "Status":
                                    query = query.OrderByDescending(p => p.Status);
                                    break;
                            }
                            break;
                        default:

                            break;
                    }
                }
                if (string.IsNullOrEmpty(a.SortProperty))
                {
                    query = query.OrderBy(p => p.ProjectNumber);
                }
                return query.ToPagedList(a.Page, a.ItemsPerPage);
            }
        }

        public void Delete(Guid id)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {

                var projectToDelete = unitOfWork.Project.Get().FirstOrDefault(p => p.ID == id);
                if (projectToDelete == null)
                {
                    return;
                }

                unitOfWork.Project.Remove(projectToDelete.ID);
                var filtered = unitOfWork.ProjectEmployee.Get().Where(pe => pe.ProjectId == id).ToList();
                foreach (ProjectEmployee exProjectEmployee in filtered)
                {
                    unitOfWork.ProjectEmployee.Remove(exProjectEmployee.ID);
                }
                unitOfWork.Commit();
            }
        }

        public void DeleteRange(IEnumerable<Guid> ids)
        {
            foreach (Guid id in ids)
            {
                Delete(id);
            }
        }

        private void Validate(Project a, UnitOfWork unitOfWork, bool editMode)
        {
            if (editMode == false)
            {
                bool foundProject = unitOfWork.Project.Get().Any(p => p.ProjectNumber == a.ProjectNumber);

                if (foundProject)
                {
                    var message = "The project number already existed. Please select a different project number.";
                    Console.WriteLine(message);
                    throw new InvalidProjectNumberException(message);
                }
            }
            if (a.EndDate != null && a.EndDate < a.StartDate)
            {
                var message = "End date must be later than start date";
                Console.WriteLine(message);
                throw new InvalidEndDateException(message);
            }
        }

        private IList<Employee> ValidateMembers(IEnumerable<string> members, UnitOfWork unitOfWork)
        {
            List<string> invalidVisa = new List<string>();
            List<Employee> validVisa = new List<Employee>();

            foreach (string member in members)
            {
                var foundEmployee = unitOfWork.Employee.Get().FirstOrDefault(e => e.Visa == member);
                if (foundEmployee == null)
                {
                    invalidVisa.Add(member);
                }
                else
                {
                    validVisa.Add(foundEmployee);
                }
            }
            if (invalidVisa.Count() > 0)
            {
                var message = "The following visas do not exist: " + string.Join(",", invalidVisa);
                Console.WriteLine(message);
                throw new InvalidVisaException(message);
            }
            return validVisa;
        }
    }
}

